using Customize.Domain.DataObject;
using Customize.Domain.DataObject.Query;
using Customize.Domain.Entities;

namespace Customize.Domain.Services
{
    public interface ICustomerService
    {
        Task<Result> SaveAsync(Customer customer);
        Task<Result<QueryResult<Customer>>> ListAsync(CustomerQueryParam queryParam);
        Task<Result<Customer>> FindAsync(string id);
        Task<Result> UpdateAsync(Customer customer);
        Task<Result> DeleteAsync(string id);
    }
}