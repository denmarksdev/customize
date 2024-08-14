using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Customize.Domain.DataObject.Query;
using Customize.Domain.Entities;
using Customize.Domain.Extensions;
using Customize.Domain.Repositories;
using Customize.Infra.Extensions;

namespace Customize.Infra.Repositories
{
    public class CustomerRepository(IAmazonDynamoDB amazonDynamoDB) : BaseRepository(amazonDynamoDB), ICustomerRepository
    {
        public Task DeleteAsync(string id)
        {
            return Table.DeleteItemAsync(id.MapCustomerPK(), TableExtensions.MapCustomerSK());
        }

        public async Task<Customer?> FindAsync(string id)
        {
            var doc = await Table.GetItemAsync(id.MapCustomerPK(), TableExtensions.MapCustomerSK());
            if (doc == null) return default;

            return doc.MapToCustomer();
        }

        public async Task<QueryResult<Customer>> ListAsync(CustomerQueryParam queryParam)
        {
            if (queryParam.HasId)
            {
                var customer = await FindAsync(queryParam.Id!);

                return new QueryResult<Customer>
                {
                    Items = customer == null ? [] : [customer],
                };
            }

            var keyExpression = new Expression();

            keyExpression.ExpressionAttributeNames.Add("#sk", TableExtensions.SKName);
            keyExpression.ExpressionAttributeNames.Add("#date", nameof(Customer.CreatedAt));
            keyExpression.ExpressionAttributeValues.Add(":sk", TableExtensions.MapCustomerSK());
            keyExpression.ExpressionAttributeValues.Add(":startdate", queryParam.DateRange.Start.Date.ToString("O"));
            keyExpression.ExpressionAttributeValues.Add(":enddate", queryParam.DateRange.End.Date.AddHours(23).AddMinutes(59).AddSeconds(59).ToString("O"));
            keyExpression.ExpressionStatement = "#sk = :sk AND #date BETWEEN :startdate AND :enddate";

            var filterExpresion = new Expression();
            if (queryParam.HasName)
            {
                filterExpresion.ExpressionAttributeNames.Add("#name", nameof(Customer.Name));
                filterExpresion.ExpressionAttributeValues.Add(":name", queryParam.Name);
                filterExpresion.ExpressionStatement = "#name = :name";
            }

            var queryConfig = new QueryOperationConfig
            {
                KeyExpression = keyExpression,
                FilterExpression = filterExpresion,
                IndexName = TableExtensions.SkDateIndex,
                PaginationToken = queryParam.PaginationToken,
                Limit = queryParam.Limit,
            };

            var search = Table.Query(queryConfig);


            var customers = new List<Customer>();
            do
            {
                var docs = await search.GetNextSetAsync();
                if (docs.Count > 0)
                {
                    customers.AddRange(docs.ConvertAll(d => d.MapToCustomer()));
                }

            } while (!search.IsDone && customers.Count == 0);

            return new QueryResult<Customer>
            {
                Items = customers,
                LastKey = search.PaginationToken
            };
        }

        public async Task SaveAsync(Customer customer)
        {
            var customerPut = new PutItemRequest
            {
                TableName = TableExtensions.CustomizeTable,
                Item = customer.MapToDocument().ToAttributeMap(),
                ConditionExpression = "attribute_not_exists(#PKNAME)",
                ExpressionAttributeNames = new Dictionary<string, string>
                {
                    { "#PKNAME", TableExtensions.PKName },
                }
            };

            await client.PutItemAsync(customerPut);
        }

        public async Task UpdateAsync(Customer customer)
        {
            var customerPut = new PutItemRequest
            {
                TableName = TableExtensions.CustomizeTable,
                Item = customer.MapToDocument().ToAttributeMap(),
                ConditionExpression = "attribute_exists(#PKNAME)",
                ExpressionAttributeNames = new Dictionary<string, string>
                {
                    { "#PKNAME", TableExtensions.PKName },
                }
            };

            await client.PutItemAsync(customerPut);
        }
    }
}
