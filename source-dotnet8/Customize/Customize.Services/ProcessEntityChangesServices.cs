using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.DynamoDBEvents;
using Customize.Domain.DataObject;
using Customize.Domain.DataObject.File;
using Customize.Domain.Entities;
using Customize.Domain.Extensions;
using Customize.Domain.Repositories;
using Customize.Domain.Services;
using Customize.Infra.Extensions;

namespace Customize.Services
{
    public class ProcessEntityChangesServices : IProcessEntityChangesServices
    {
        public const string InsertEvent = "INSERT";
        public const string ModifyEvent = "MODIFY";
        public const string RemoveEvent = "REMOVE";
        private readonly IFileRepository _fileRepository;

        public ProcessEntityChangesServices(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<Result> ExecuteAsync(DynamoDBEvent dynamoDBEvent)
        {
            var result = new Result() { Success = true };

            foreach (var record in dynamoDBEvent.Records)
            {
                var oldImage = record.Dynamodb.OldImage;
                var newImage = record.Dynamodb.NewImage;

                var oldCustomer = ParseToCustomer(oldImage);
                var newCustomer = ParseToCustomer(newImage);

                if (oldCustomer == null && newCustomer == null)
                {
                    continue;
                }

                Customer selectedCustomer;

                switch (record.EventName)
                {
                    case InsertEvent:
                        selectedCustomer = newCustomer!;
                        break;
                    case ModifyEvent:
                        selectedCustomer = oldCustomer!;
                        break;
                    case RemoveEvent:
                        selectedCustomer = (oldCustomer ?? newCustomer)!;
                        break;
                    default:
                        continue;
                }

                var filename = $"customers/{selectedCustomer.Name}_{selectedCustomer.Id}_{record.EventName}.json";
                var fileSerialized = new CustomerFile(record.EventName, selectedCustomer).Serialize();

                var fileResult = await _fileRepository.PutAsync(new PutFile(
                    key: filename,
                    bucketName: "denmarksdev-customize",
                    fileSerialized
                    ));

                if (!fileResult.Success) 
                {
                    Console.WriteLine("S3 Result {0}", fileResult.Serialize());
                    result.Success = false;
                }
            }

            return result;
        }

        private Customer? ParseToCustomer(Dictionary<string, Amazon.Lambda.DynamoDBEvents.DynamoDBEvent.AttributeValue> attributes) 
        {
            if (attributes == null) return null;

            var doc = Document.FromAttributeMap(attributes.ToDictionary(d => d.Key, d => new AttributeValue(d.Value.S)));

            return  doc.MapToCustomer();
        }
    }
}
