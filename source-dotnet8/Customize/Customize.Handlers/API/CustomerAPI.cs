using Amazon.Lambda.Annotations.APIGateway;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Core;
using Customize.Handlers.API.Base;

namespace Customize.Handlers.API
{
    public class CustomerAPI : BaseAPI
    {
        [LambdaFunction(ResourceName = "CustomerListAPI")]
        [RestApi(LambdaHttpMethod.Get, "/api/v1/customers")]
        public IHttpResult Get(ILambdaContext context)
        {
            context.Logger.LogInformation("Handling the 'Get' Request");

            return HttpResults.Ok("GET LIST");
        }

        [LambdaFunction(ResourceName = "CustomerGetByIdAPI")]
        [RestApi(LambdaHttpMethod.Get, "/api/v1/customers/{id}")]
        public IHttpResult GetById(ILambdaContext context, string id)
        {
            context.Logger.LogInformation("Handling the 'Get' Request");

            return HttpResults.Ok($"GET {id}");
        }

        [LambdaFunction(ResourceName = "CustomerPostAPI")]
        [RestApi(LambdaHttpMethod.Post, "/api/v1/customers")]
        public IHttpResult Post(ILambdaContext context)
        {
            context.Logger.LogInformation("Handling the 'POST' Request");

            return HttpResults.Ok("POST");
        }

        [LambdaFunction(ResourceName = "CustomerPutAPI")]
        [RestApi(LambdaHttpMethod.Put, "/api/v1/customers")]
        public IHttpResult Put(ILambdaContext context)
        {
            context.Logger.LogInformation("Handling the 'POST' Request");

            return HttpResults.Ok("PUT");
        }

        [LambdaFunction(ResourceName = "CustomerDeleteAPI")]
        [RestApi(LambdaHttpMethod.Delete, "/api/v1/customers")]
        public IHttpResult Delete(ILambdaContext context)
        {
            context.Logger.LogInformation("Handling the 'POST' Request");

            return HttpResults.Ok("Delete");
        }
    }
}
