using Amazon.Lambda.Annotations;
using Amazon.Lambda.Core;
using Amazon.Lambda.DynamoDBEvents;
using Customize.Domain.Extensions;
using Customize.Domain.Services;
using Customize.Handlers.Base;

namespace Customize.Handlers.Stream
{
    public class CustomerStream : BaseLambda
    {
        [LambdaFunction(ResourceName = "CustomerStreamProcess")]
        public async Task Receive(DynamoDBEvent dynamoEvent,ILambdaContext context, [FromServices] IProcessEntityChangesServices processEntityChangesServices)
        {
            context.Logger.LogInformation($"Events {dynamoEvent.Serialize()}" );
            var result = await processEntityChangesServices.ExecuteAsync(dynamoEvent);
            
            context.Logger.LogInformation($"Received {result.Serialize()}");
        }
    }
}
