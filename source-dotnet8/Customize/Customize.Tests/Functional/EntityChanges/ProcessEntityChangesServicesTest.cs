using Amazon.Lambda.DynamoDBEvents;
using Customize.Tests.Extensions;
using Customize.Tests.Mocks;

namespace Customize.Tests.Functional.EntityChanges
{
    public class ProcessEntityChangesServicesTest
    {
        [Fact]
        public async Task ShouldProcessInsertedCustomer()
        {
            // Arrange
            var insertedEvent = "Streams/customer-insert.json".FromFixture<DynamoDBEvent>()!; 
           
            // Act
            var result = await _mocks.ProcessChangeService.ExecuteAsync(insertedEvent);

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public void ShouldProcessUpdatedCustomer()
        {
            // Arrange
            // Act
            // Assert
        }

        [Fact]
        public void ShouldProcessDeleteCustomer()
        {
            // Arrange
            // Act
            // Assert
        }

        #region Setup

        private readonly CustomizeMocks _mocks;

        public ProcessEntityChangesServicesTest()
        {
            _mocks = new CustomizeMocks();
        }
        #endregion Setup
    }
}