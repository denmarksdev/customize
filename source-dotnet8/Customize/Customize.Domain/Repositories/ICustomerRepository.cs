using Customize.Domain.DataObject.Query;
using Customize.Domain.Entities;

namespace Customize.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task SaveAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task<QueryResult<Customer>> ListAsync(CustomerQueryParam queryParam);
        Task<Customer?> FindAsync(string id);
        Task DeleteAsync(string id);
    }
}