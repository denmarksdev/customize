using Amazon.Lambda.DynamoDBEvents;
using Customize.Domain.DataObject;

namespace Customize.Domain.Services
{
    public interface IProcessEntityChangesServices
    {
        Task<Result> ExecuteAsync(DynamoDBEvent dynamoDBEvent);
    }
}
