using Amazon;
using Amazon.S3;

namespace Customize.Infra.Factories
{
    public static class S3Factory
    {
        public static IAmazonS3 Build() 
        {
            return new AmazonS3Client(RegionEndpoint.USEast1);
        }
    }
}