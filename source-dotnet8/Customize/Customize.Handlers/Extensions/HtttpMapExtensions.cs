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

            if (response.Success)
                return HttpResults.Ok(jsonData);

            return HttpResults.BadRequest(jsonData);
        }
    }
}
