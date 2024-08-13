using Customize.Domain.DataObject;
using Customize.Domain.Entities;

namespace Customize.Domain.Services
{
    public interface ICustomerService
    {
        Task<Result> SaveAsync(Customer customer);
    }
}