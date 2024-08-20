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
            FileRepositoryMock = new Mock<IFileRepository>();
            ProcessChangeService = new ProcessEntityChangesServices(FileRepositoryMock.Object);
        }

        public Mock<ICustomerRepository> CustomerRepositoryMock { get; }
        public Mock<IFileRepository> FileRepositoryMock { get; }
        public CustomerService CustomerService { get; }
        public ProcessEntityChangesServices ProcessChangeService { get; }
    }
}
