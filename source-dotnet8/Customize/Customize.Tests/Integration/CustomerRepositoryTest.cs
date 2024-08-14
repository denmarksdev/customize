using Customize.Domain.DataObject.Query;
using Customize.Domain.Entities;
using Customize.Infra.Factories;
using Customize.Infra.Repositories;
using Customize.Tests.Mocks.Builders;
using System.ComponentModel;

namespace Customize.Tests.Integration
{
    public class CustomerRepositoryTest
    {
        #region CrudFlowTest 

        [Fact]
        [Category("Integration test")]
        public async Task CrudFlowTest()
        {
            var customer = await ShouldSave();
            await ShouldFind(customer);
            await ShouldUpdate(customer);
            await ShouldDelete(customer);
        }

        private async Task<Customer> ShouldSave()
        {
            // Arrange
            var customer = new Customer(
                id: "101",
                name: "Jhon Doe",
                cellphone: "5511988889999",
                email: "jhondoe@teste.com",
                createdAt: DateTime.Now
                );

            // Act
            await _customerRepository.SaveAsync(customer);

            return customer;
        }

        private async Task ShouldFind(Customer customer)
        {
            // Arrange
            var id = customer.Id;

            // Act
            var customerPersisted = await _customerRepository.FindAsync(id);

            // Assert
            Assert.NotNull(customerPersisted);
        }

        private async Task ShouldUpdate(Customer customer)
        {
            // Arrange
            customer.Name += "(update)";

            // Act
            await _customerRepository.UpdateAsync(customer);

            var persisted = await _customerRepository.FindAsync(customer.Id);

            // Assert
            Assert.NotNull(persisted);
            Assert.Equal(customer.Name, persisted.Name);
        }

        #endregion FlowTest 

        /// <summary>
        /// Simula a listem de cliente e paginação com scroll infinito.
        /// </summary>
        [Fact]
        [Category("Integration test")]
        public async void ShouldList()
        {
            // Arrange
            var customerList = new GenerateCustomerListBuilder()
               .AddSimpleCustomer("Jhon")
               .AddSimpleCustomer("Maria")
               .Build();

            var queryParam = new CustomerQueryParam(new DateRangeQueryParam(start: DateTime.Now, end: DateTime.Now))
            {
                Limit = 1
            };

            var customerResultList = new List<Customer>();

            // Act
            foreach (var customer in customerList)
            {
                await _customerRepository.SaveAsync(customer);
            }

            /* Valida paginação */
            var queryResult = await _customerRepository.ListAsync(queryParam);
            customerResultList.AddRange(queryResult.Items);
            
            queryParam.PaginationToken = queryResult.LastKey;

            queryResult = await _customerRepository.ListAsync(queryParam);
            customerResultList.AddRange(queryResult.Items);

            queryParam.PaginationToken = queryResult.LastKey;

            queryResult = await _customerRepository.ListAsync(queryParam);
            customerResultList.AddRange(queryResult.Items);

            /* Limpa base de dados de teste */
            foreach (var customer in customerList)
            {
                await _customerRepository.DeleteAsync(customer.Id);
            }

            // Assert
            Assert.Equal(2, customerResultList.Count);
            Assert.False(queryResult.HasMore);
        }

        private async Task ShouldDelete(Customer customer)
        {
            // Arrange
            var id = customer.Id;

            // Act
            await _customerRepository.DeleteAsync(id);

            var persisted = await _customerRepository.FindAsync(customer.Id);

            // Assert
            Assert.Null(persisted);
        }

        #region Setup
        private readonly CustomerRepository _customerRepository;

        public CustomerRepositoryTest()
        {
            var dynamoClient = DynamoDBFactory.BuildLocalhost();
            _customerRepository = new CustomerRepository(dynamoClient);
        }
        #endregion Setup
    }
}
