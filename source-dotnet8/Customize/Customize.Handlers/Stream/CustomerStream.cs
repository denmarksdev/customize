using Amazon.Lambda.Annotations;
using Amazon.Lambda.Core;
using Amazon.Lambda.DynamoDBEvents;
using Customize.Domain.Extensions;
using Customize.Handlers.Base;

namespace Customize.Handlers.Stream
{
    public class CustomerStream : BaseLambda
    {
        [LambdaFunction(ResourceName = "CustomerStreamProcess")]
        public void Receive(DynamoDBEvent dynamoEvent,ILambdaContext context)
        {
            context.Logger.LogInformation($"Events {dynamoEvent.Serialize()}" );
        }
    }
}
