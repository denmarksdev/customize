using Amazon.Lambda.Annotations.APIGateway;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Core;
using Customize.Handlers.Base;
using Amazon.Lambda.APIGatewayEvents;
using Customize.Domain.Extensions;
using Customize.Domain.Services;
using Customize.Contracts.Customer;
using Customize.Contracts.Extensions;

namespace Customize.Handlers.API
{
    public class CustomerAPI : BaseLambda
    {
        [LambdaFunction(ResourceName = "CustomerListAPI")]
        [RestApi(LambdaHttpMethod.Get, "/api/v1/customers")]
        public async Task<IHttpResult> Get(ILambdaContext context, APIGatewayProxyRequest request , [FromServices] ICustomerService customerService)
        {
            context.Logger.LogInformation($"List Customer {request.QueryStringParameters?.Serialize()}");

            var customerQueryParam = request.QueryStringParameters?
                .MapListCustomerRequest()
                .MapCustomerQueryParam();

            if (customerQueryParam == null) return HttpResults.BadRequest($"Payload para consulta de clientes inválido. {request.Body}");

            var listResult = await customerService.ListAsync(customerQueryParam);

            return MapResult(listResult);
        }

        [LambdaFunction(ResourceName = "CustomerGetByIdAPI")]
        [RestApi(LambdaHttpMethod.Get, "/api/v1/customers/{id}")]
        public async Task <IHttpResult> GetById(ILambdaContext context, string id, [FromServices] ICustomerService customerService)
        {
            context.Logger.LogInformation($"GET customer id {id}");

            var findResult = await customerService.FindAsync(id);

            return MapResult(findResult);
        }

        [LambdaFunction(ResourceName = "CustomerPostAPI")]
        [RestApi(LambdaHttpMethod.Post, "/api/v1/customers")]
        public async Task<IHttpResult> Post([FromBody] APIGatewayProxyRequest request, ILambdaContext context, [FromServices] ICustomerService customerService)
        {
            if (request == null) return HttpResults.BadRequest("Payload para novo cliente inválido.");

            context.Logger.LogInformation($"New customer {request?.Serialize()}");

            var customer = request!.Body.Deserialize<PostCustomerRequest>()?.Map();

            if (customer == null) return HttpResults.BadRequest($"Payload para novo cliente inválido. {request.Serialize()}");

            var saveResult = await customerService.SaveAsync(customer);

            return MapResult(saveResult);
        }

        [LambdaFunction(ResourceName = "CustomerPutAPI")]
        [RestApi(LambdaHttpMethod.Put, "/api/v1/customers")]
        public async Task<IHttpResult> Put([FromBody] APIGatewayProxyRequest request, ILambdaContext context, [FromServices] ICustomerService customerService)
        {
            if (request == null) return HttpResults.BadRequest("Payload para novo cliente inválido.");

            context.Logger.LogInformation($"Update customer {request?.Serialize()}");

            var customer = request!.Body.Deserialize<PutCustomerRequest>()?.Map();

            if (customer == null) return HttpResults.BadRequest($"Payload para novo cliente inválido. {request.Serialize()}");

            var saveResult = await customerService.UpdateAsync(customer);

            return MapResult(saveResult);
        }

        [LambdaFunction(ResourceName = "CustomerDeleteAPI")]
        [RestApi(LambdaHttpMethod.Delete, "/api/v1/customers/{id}")]
        public async Task<IHttpResult> Delete(ILambdaContext context, string id, [FromServices] ICustomerService customerService)
        {
            context.Logger.LogInformation($"Delete customer ID {id}");

            var deleteResult = await customerService.DeleteAsync(id);

            return MapResult(deleteResult);
        }
    }
}
