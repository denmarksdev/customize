using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2;
using Customize.Infra.Factories;
using Customize.Infra.Extensions;
using Customize.Domain.Entities;
using Customize.Infra.Repositories;

namespace Customize.Tests.Integration
{
    public class DymamoStreamTest
    {
        [Fact]
        public async Task ShouldCreate()
        {
            // Arrange
            var request = new CreateTableRequest
            {
                TableName = TableExtensions.CustomizeTable,
                KeySchema =
                [
                    new KeySchemaElement
                    {
                        AttributeName = TableExtensions.PKName,
                        KeyType = KeyType.HASH
                    },
                    new KeySchemaElement
                    {
                        AttributeName = TableExtensions.SKName,
                        KeyType = KeyType.RANGE
                    }
                ],
                AttributeDefinitions =
                [
                    new AttributeDefinition
                    {
                        AttributeName = TableExtensions.PKName,
                        AttributeType = ScalarAttributeType.S
                    },
                    new AttributeDefinition
                    {
                        AttributeName = TableExtensions.SKName,
                        AttributeType = ScalarAttributeType.S
                    },
                    new AttributeDefinition
                    {
                        AttributeName = nameof(Customer.CreatedAt),
                        AttributeType = ScalarAttributeType.S
                    }
                ],
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 12,
                    WriteCapacityUnits = 12
                },
                StreamSpecification = new StreamSpecification
                {
                    StreamEnabled = true,
                    StreamViewType = StreamViewType.NEW_IMAGE
                },
                GlobalSecondaryIndexes =
                [
                    new GlobalSecondaryIndex
                    {
                        IndexName = TableExtensions.InverterIndexSkPkIndex,
                        KeySchema =
                        [
                            new KeySchemaElement
                            {
                                AttributeName = TableExtensions.SKName,
                                KeyType = KeyType.HASH
                            },
                            new KeySchemaElement
                            {
                                AttributeName = TableExtensions.PKName,
                                KeyType = KeyType.RANGE
                            },
                        ],
                        Projection = new Projection
                        {
                            ProjectionType = ProjectionType.ALL
                        },
                        ProvisionedThroughput = new ProvisionedThroughput
                        {
                            ReadCapacityUnits = 1,
                            WriteCapacityUnits = 1
                        }
                    },
                    new GlobalSecondaryIndex
                    {
                        IndexName = TableExtensions.SkDateIndex,
                        KeySchema =
                        [
                            new KeySchemaElement
                            {
                                AttributeName = TableExtensions.SKName,
                                KeyType = KeyType.HASH
                            },
                            new KeySchemaElement
                            {
                                AttributeName = nameof(Customer.CreatedAt),
                                KeyType = KeyType.RANGE
                            },
                        ],
                        Projection = new Projection
                        {
                            ProjectionType = ProjectionType.ALL
                        },
                        ProvisionedThroughput = new ProvisionedThroughput
                        {
                            ReadCapacityUnits = 1,
                            WriteCapacityUnits = 1
                        }
                    }
                ]
            };

            // Act
            var createTableResponse = await _dynamoClient.CreateTableAsync(request);

            var customerRepository = new CustomerRepository(_dynamoClient);
            var customerTest = await customerRepository.FindAsync("fake-id");

            // Assert
            Assert.Null(customerTest);
            Assert.NotNull(createTableResponse);
        }

        #region Setup
        private readonly AmazonDynamoDBClient _dynamoClient;

        public DymamoStreamTest()
        {
            _dynamoClient = DynamoDBFactory.BuildLocalhost();
        }
        #endregion Setup
    }
}
