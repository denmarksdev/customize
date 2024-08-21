using Amazon.Lambda.Annotations.APIGateway;
using Customize.Contracts.Base;
using Customize.Domain.Extensions;

namespace Customize.Handlers.Extensions
{
    public static class HtttpMapExtensions
    {
        public static IHttpResult MapAPIResponse(this BaseResponse response) 
        {
            var jsonData = response.Serialize();

            var result = response.Success ? HttpResults.Ok(jsonData) : HttpResults.BadRequest(jsonData);

            result = result.MapOptions();

            return result;
        }

        public static IHttpResult MapOptions(this IHttpResult httpResult) 
        {
            httpResult.AddHeader("Access-Control-Allow-Headers", "*");
            httpResult.AddHeader("Access-Control-Allow-Origin", "*");
            httpResult.AddHeader("Access-Control-Allow-Methods", "OPTIONS,POST,GET,DELETE,PUT");

            return httpResult;
        }
    }
}
