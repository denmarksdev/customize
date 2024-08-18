using Amazon.Lambda.Annotations.APIGateway;
using Amazon.Lambda.Core;
using Customize.Contracts.Extensions;
using Customize.Domain.DataObject;
using Customize.Handlers.Extensions;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Customize.Handlers.Base
{
    public abstract class BaseLambda
    {

        protected IHttpResult MapResult(Result result, string? message = null) 
        {
            var baseResponse = result.MapBaseResponse(message);

            return baseResponse.MapAPIResponse();
        }

        protected IHttpResult MapResult<T>(Result<T> result, string? message = null)
        {
            var baseResponse = result.MapBaseResponse(message);

            return baseResponse.MapAPIResponse();
        }
    }
}
