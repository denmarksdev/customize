using Customize.Domain.Entities;
using Customize.Tests.Mocks;

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
        }

        /// <summary>
        /// 1.2 - Deve salvar os dados do cliente.
        /// </summary>
        [Fact]
        public void ShouldSave()
        {
            // Arrange
            // Act
            // Assert
        }

        /// <summary>
        /// 2.1 - Deve consultar clientes de forma paginada.
        /// </summary>
        [Fact]
        public void ShouldList()
        {
            // Arrange
            // Act
            // Assert
        }

        /// <summary>
        /// 2.2 - Deve consultar cliente por ID.
        /// </summary>
        [Fact]
        public void ShouldGetByID()
        {
            // Arrange
            // Act
            // Assert
        }

        /// <summary>
        /// 3.1 - Deve validar os dados obrigatórios do cliente antes de atualizar os dados.
        /// </summary>
        [Fact]
        public void ShouldValidataDataBeforeUpdate()
        {
            // Arrange
            // Act
            // Assert
        }

        /// <summary>
        /// 3.2 - Deve salvar os dados do cliente.
        /// </summary>
        [Fact]
        public void ShouldUpdate()
        {
            // Arrange
            // Act
            // Assert
        }

        /// <summary>
        /// 4.1 - Deve validar dados do cliente antes de excluir.
        /// </summary>
        [Fact]
        public void ShouldValidataDataBeforeDelete()
        {
            // Arrange
            // Act
            // Assert
        }

        /// <summary>
        /// 4.2 - Deve excluir os dados do cliente.
        /// </summary>
        [Fact]
        public void ShouldDelete()
        {
            // Arrange
            // Act
            // Assert
        }


        #region Setup
        private readonly CustomizeMocks _mocks = new(); 
        #endregion Setup
    }
}
