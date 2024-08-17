using Amazon.DynamoDBv2;
using Amazon.Runtime;

namespace Customize.Infra.Factories
{
    public static class DynamoDBFactory
    {
        public static AmazonDynamoDBClient BuildLocalhost() 
        {
            AmazonDynamoDBConfig config = new AmazonDynamoDBConfig
            {
                ServiceURL = "http://localhost:8000"
            };

            var creds = new BasicAWSCredentials("xxx", "xxx");
            var client = new AmazonDynamoDBClient(creds, config)!;

            return client;
        }

        public static AmazonDynamoDBClient Build()
        {
            var creds = new BasicAWSCredentials("xxx", "xxx");
            var client = new AmazonDynamoDBClient(creds)!;

            return client;
        }

    }
}
