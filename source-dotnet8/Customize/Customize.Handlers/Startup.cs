using Amazon.DynamoDBv2;
using Amazon.Lambda.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Amazon.Extensions.NETCore.Setup;
using Amazon;
using Customize.Domain.Repositories;
using Customize.Infra.Repositories;
using Customize.Domain.Services;
using Amazon.Lambda.Core;
using Customize.Services;

namespace Customize.Handlers;

[LambdaStartup]
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        //// Example of creating the IConfiguration object and
        //// adding it to the dependency injection container.
        //var builder = new ConfigurationBuilder()
        //                    .AddJsonFile("appsettings.json", true);

        LambdaLogger.Log("INIT com customer!!!!!!!!!!!!!!!!!!!!!!");

        #region AWS Services
        services.AddAWSService<IAmazonDynamoDB>(new AWSOptions { Region = RegionEndpoint.USEast1 });
        #endregion AWS Services

        #region Repositories
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        #endregion Repositories

        #region Services
        services.AddScoped<ICustomerService, CustomerService>();
        #endregion Services
    }
}
