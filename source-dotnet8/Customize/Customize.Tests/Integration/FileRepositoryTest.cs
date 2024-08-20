using Customize.Domain.DataObject.File;
using Customize.Domain.Extensions;
using Customize.Infra.Factories;
using Customize.Infra.Repositories;
using Customize.Tests.Mocks.Builders;

namespace Customize.Tests.Integration
{
    public class FileRepositoryTest
    {
        [Fact]
        public async Task ShouldSave() 
        {
            // Arrange
            var customerList = new GenerateCustomerListBuilder()
                .AddSimpleCustomer("Jhon Doe")
                .Build();
            
            var customer = customerList[0];

            var key = Guid.Empty + DateTime.Now.ToString("O");

            var input = new PutFile(
                key: "customers/" + key + ".json",
                bucketName: "denmarksdev-customize",
                message:  new { action = "INSERT", customer  }.Serialize()
                );

            // Act
            var result = await _repository.PutAsync(input);
            
            // Assert
            Assert.True(result.Success);
        }

        #region Setup
        private readonly FileRepositoryS3 _repository;
        public FileRepositoryTest()
        {
            _repository = new FileRepositoryS3(S3Factory.Build());
        }
        #endregion Setup
    }
}
