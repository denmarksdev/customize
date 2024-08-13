using Customize.Domain.DataObject.Query;
using Customize.Domain.Entities;
using Customize.Tests.Mocks;
using Customize.Tests.Mocks.Builders;
using Moq;

namespace Customize.Tests.Functional.CustomerServices
{
    public class CustomerServiceTests
    {
        /// <summary>
        /// 1.1 - Deve validar os dados obrigatórios do cliente antes de salvar os dados.
        /// </summary>
        [Fact]
        public async Task ShoudValidataDataBeforeSave()
        {
            // Arrange
            var customer = new Customer(
                id: string.Empty,
                name: string.Empty,
                cellphone:"11111",
                email: "jhondoe.teste.com",
                createdAt: DateTime.MinValue
                );

            // Act
            var result = await _mocks.CustomerService.SaveAsync(customer);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Sucess);
            Assert.Equal("Id do cliente é obrigatório.", result.Errors[nameof(Customer.Id)]);
            Assert.Equal("Nome do cliente é obrigatório.", result.Errors[nameof(Customer.Name)]);
            Assert.Equal("Número do celular inválido. Pais, DD, e numero são obrigatórios.", result.Errors[nameof(Customer.Cellphone)]);
            Assert.Equal("Email inválido", result.Errors[nameof(Customer.Email)]);
            Assert.Equal("Data do cadastro inválido.", result.Errors[nameof(Customer.CreatedAt)]);

            _mocks.CustomerRepositoryMock.Verify(m => m.SaveAsync(customer), Times.Never);
        }

        /// <summary>
        /// 1.2 - Deve salvar os dados do cliente.
        /// </summary>
        [Fact]
        public async Task ShouldSave()
        {
            // Arrange
            var customer = new Customer(
                id: Guid.NewGuid().ToString(),
                name: "Jhon Doe",
                cellphone: "5511988889999",
                email: "jhondoe@teste.com",
                createdAt: DateTime.Now
                );

            // Act
            var result = await _mocks.CustomerService.SaveAsync(customer);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Sucess);
            Assert.Empty(result.Errors);

            _mocks.CustomerRepositoryMock.Verify(m => m.SaveAsync(customer),Times.Once);
        }

        /// <summary>
        /// 2.1 - Deve consultar clientes de forma paginada.
        /// </summary>
        [Fact]
        public async void ShouldList()
        {
            // Arrange
            var customerList = new GenerateCustomerListBuilder()
                .AddSimpleCustomer("Jhon Doe")
                .AddSimpleCustomer("Philip J")
                .AddSimpleCustomer("Tomas Anderson")
                .Build();

            var queryResult = new QueryResult<Customer> { Items = customerList };

            var queryParam = new CustomerQueryParam();

            _mocks.CustomerRepositoryMock
                .Setup(c => c.ListAsync(queryParam))
                .ReturnsAsync(queryResult);

            // Act
            var result = await _mocks.CustomerService.ListAsync(queryParam);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.True(result.Sucess);
            Assert.Equal(3, result.Data.Items.Count);

            _mocks.CustomerRepositoryMock.Verify(m => m.ListAsync(queryParam), Times.Once);
        }

        /// <summary>
        /// 2.2 - Deve consultar cliente por ID.
        /// </summary>
        [Fact]
        public async Task ShouldGetByID()
        {
            // Arrange
            var customerList = new GenerateCustomerListBuilder()
                .AddSimpleCustomer("Jhon Doe")
                .Build();

            var customer =customerList[0];

            var queryResult = new QueryResult<Customer> { Items = customerList };

            var queryParam = new CustomerQueryParam();

            _mocks.CustomerRepositoryMock
                .Setup(c => c.FindAsync(customer.Id))
                .ReturnsAsync(customer);

            // Act
            var result = await _mocks.CustomerService.FindAsync(customer.Id);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.True(result.Sucess);
            Assert.Equal(customer.Name, result.Data.Name);

            _mocks.CustomerRepositoryMock.Verify(m => m.FindAsync(customer.Id), Times.Once);
        }

        /// <summary>
        /// 3.1 - Deve validar os dados obrigatórios do cliente antes de atualizar.
        /// </summary>
        [Fact]
        public async Task ShouldValidataDataBeforeUpdate()
        {
            // Arrange
            var customerList = new GenerateCustomerListBuilder()
                .AddSimpleCustomer("Jhon Doe")
                .Build();

            var customer = customerList[0]; 
            Customer? customerOnDB = null;

            _mocks.CustomerRepositoryMock.Setup(c=> c.FindAsync(customer.Id))
                .ReturnsAsync(customerOnDB);

            // Act
            var result = await _mocks.CustomerService.UpdateAsync(customer);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Sucess);
            Assert.Single(result.Errors);
            Assert.Contains("Cliente não foi encontrado. ID:", result.Errors[nameof(Customer.Id)]);

            _mocks.CustomerRepositoryMock.Verify(m => m.FindAsync(customer.Id), Times.Once);
            _mocks.CustomerRepositoryMock.Verify(m => m.UpdateAsync(customer), Times.Never);
        }

        /// <summary>
        /// 3.2 - Deve atualzar os dados do cliente.
        /// </summary>
        [Fact]
        public async Task ShouldUpdate()
        {
            // Arrange
            var customerList = new GenerateCustomerListBuilder()
                .AddSimpleCustomer("Jhon Doe")
                .Build();

            var customer = customerList[0];

            _mocks.CustomerRepositoryMock.Setup(c => c.FindAsync(customer.Id))
                .ReturnsAsync(customer);

            // Act
            var result = await _mocks.CustomerService.UpdateAsync(customer);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Sucess);
            Assert.Empty(result.Errors);

            _mocks.CustomerRepositoryMock.Verify(m => m.FindAsync(customer.Id), Times.Once);
            _mocks.CustomerRepositoryMock.Verify(m => m.UpdateAsync(customer), Times.Once);
        }

        /// <summary>
        /// 4.1 - Deve validar dados do cliente antes de excluir.
        /// </summary>
        [Fact]
        public async Task ShouldValidataDataBeforeDelete()
        {
            // Arrange
            var customerList = new GenerateCustomerListBuilder()
                .AddSimpleCustomer("Jhon Doe")
                .Build();

            var customer = customerList[0];
            Customer? customerOnDB = null;

            _mocks.CustomerRepositoryMock.Setup(c => c.FindAsync(customer.Id))
                .ReturnsAsync(customerOnDB);

            // Act
            var result = await _mocks.CustomerService.DeleteAsync(customer.Id);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Sucess);
            Assert.Single(result.Errors);
            Assert.Contains("Cliente não foi encontrado. ID:", result.Errors[nameof(Customer.Id)]);

            _mocks.CustomerRepositoryMock.Verify(m => m.FindAsync(customer.Id), Times.Once);
            _mocks.CustomerRepositoryMock.Verify(m => m.DeleteAsync(customer.Id), Times.Never);
        }

        /// <summary>
        /// 4.2 - Deve excluir os dados do cliente.
        /// </summary>
        [Fact]
        public async Task ShouldDelete()
        {
            // Arrange
            var customerList = new GenerateCustomerListBuilder()
                .AddSimpleCustomer("Jhon Doe")
                .Build();

            var customer = customerList[0];

            _mocks.CustomerRepositoryMock.Setup(c => c.FindAsync(customer.Id))
                .ReturnsAsync(customer);

            // Act
            var result = await _mocks.CustomerService.DeleteAsync(customer.Id);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Sucess);
            Assert.Empty(result.Errors);

            _mocks.CustomerRepositoryMock.Verify(m => m.FindAsync(customer.Id), Times.Once);
            _mocks.CustomerRepositoryMock.Verify(m => m.DeleteAsync(customer.Id), Times.Once);
        }

        #region Setup
        private readonly CustomizeMocks _mocks = new(); 
        #endregion Setup
    }
}
