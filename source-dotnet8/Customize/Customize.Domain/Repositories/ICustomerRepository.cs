using Customize.Domain.Entities;

namespace Customize.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task SaveAsync(Customer customer);
    }
}