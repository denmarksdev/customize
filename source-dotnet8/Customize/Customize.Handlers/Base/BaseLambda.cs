using Amazon.Lambda.Annotations.APIGateway;
using Amazon.Lambda.Core;
using Customize.Contracts.Base;
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

            if (!result.Success && result.Exception != null)
            {
                Console.WriteLine("Error: {0} \n StackTrace: {1}", result.Exception.Message, result.Exception.StackTrace);
            }

            return baseResponse.MapAPIResponse();
        }

        protected IHttpResult Options()
        {
           return HttpResults.Ok().MapOptions();
        }

        protected IHttpResult MapResult<T>(Result<T> result, string? message = null)
        {
            var baseResponse = result.MapBaseResponse(message);

            return baseResponse.MapAPIResponse();
        }

        protected IHttpResult BadRequest(string message)
        {
            var bad = new BaseResponse { Message = message };
            return bad.MapAPIResponse();
        }


    }
}
