using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2;
using Customize.Infra.Extensions;

namespace Customize.Infra.Repositories
{
    public class BaseRepository(IAmazonDynamoDB amazonDynamoDB)
    {
        protected readonly Table Table = Table.LoadTable(amazonDynamoDB, TableExtensions.CustomizeTable);
        protected readonly IAmazonDynamoDB client = amazonDynamoDB;
    }
}
