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
using Amazon.S3;

namespace Customize.Handlers;

[LambdaStartup]
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        #region AWS Services
        services.AddAWSService<IAmazonDynamoDB>(new AWSOptions { Region = RegionEndpoint.USEast1 });
        services.AddAWSService<IAmazonS3>(new AWSOptions { Region = RegionEndpoint.USEast1 });
        #endregion AWS Services

        #region Repositories
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IFileRepository, FileRepositoryS3>();
        #endregion Repositories

        #region Services
        services.AddScoped<ICustomerService, CustomerService>();
        #endregion Services
    }
}
