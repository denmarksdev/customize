using Customize.Domain.DataObject;
using Customize.Domain.Entities;
using Customize.Domain.Repositories;
using Customize.Domain.Services;
using Customize.Domain.Validators;

namespace Customize.Services
{
    public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;

        public async Task<Result> SaveAsync(Customer customer)
        {
            var result = new Result();

            try
            {
                var validator = new SaveCustomerValidator().Validate(customer);
                if (!validator.IsValid) 
                {
                    result.Errors = validator.Errors.ToDictionary(v => v.PropertyName, v => v.ErrorMessage);
                    return result;
                }

                

                await _customerRepository.SaveAsync(customer);

                result.Sucess = true;
            }
            catch (Exception ex)
            {
                result.Errors.Add(Result.UnmanagedError, "Erro não gerênciado ao salvar os dados do cliente.");
                result.Exception = ex;
            }

            return result;
        }
    }
}