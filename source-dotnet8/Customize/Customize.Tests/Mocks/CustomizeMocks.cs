using Customize.Domain.Repositories;
using Customize.Services;
using Moq;

namespace Customize.Tests.Mocks
{
    public class CustomizeMocks
    {
        public CustomizeMocks()
        {
            CustomerRepositoryMock = new Mock<ICustomerRepository>();
            CustomerService = new CustomerService(CustomerRepositoryMock.Object);
        }

        public Mock<ICustomerRepository> CustomerRepositoryMock { get; }
        public CustomerService CustomerService { get; }
    }
}
