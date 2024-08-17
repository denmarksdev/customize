using Amazon.Lambda.Core;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Customize.Handlers.API.Base
{
    public abstract class BaseAPI
    {
    }
}
