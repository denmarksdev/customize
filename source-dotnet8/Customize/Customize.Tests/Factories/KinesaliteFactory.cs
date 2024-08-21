using Amazon.Kinesis;

namespace Customize.Tests.Factories
{
    public static class KinesaliteFactory
    {
        public static AmazonKinesisClient BuildLocahostClient() 
        {

            var kinesisConfig = new AmazonKinesisConfig
            {
                ServiceURL = "http://localhost:4567"
            };

            var kinesisClient = new AmazonKinesisClient(kinesisConfig);

            return kinesisClient;
        }
    }
}
