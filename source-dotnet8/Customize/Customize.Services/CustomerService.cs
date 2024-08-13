using Customize.Domain.DataObject;
using Customize.Domain.DataObject.Query;
using Customize.Domain.DataObject.Validators;
using Customize.Domain.Entities;
using Customize.Domain.Extensions;
using Customize.Domain.Repositories;
using Customize.Domain.Services;
using Customize.Domain.Validators;

namespace Customize.Services
{
    public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;

        public async Task<Result> DeleteAsync(string id)
        {
            var result = new Result();

            try
            {
                var validator = await new DeleteCustomerValidatorAsync(_customerRepository)
                    .ValidateAsync(new DeleteValidatorParam(id));

                if (!validator.IsValid)
                {
                    result.Errors = validator.Errors.ToDictionary(v => v.PropertyName, v => v.ErrorMessage);
                    return result;
                }

                await _customerRepository.DeleteAsync(id);

                result.Sucess = true;
            }
            catch (Exception ex)
            {
                result.Errors.Add(Result.UnmanagedError, "Erro não gerênciado ao atualizar os dados do cliente.");
                result.Exception = ex;
            }

            return result;
        }

        public async Task<Result<Customer>> FindAsync(string id)
        {
            var result = new Result<Customer>();

            try
            {
                result.Data = await _customerRepository.FindAsync(id);
                result.Sucess = true;
            }
            catch (Exception ex)
            {
                result.Errors.Add(Result.UnmanagedError, $"Erro não gerênciado ao consultar o cliente. ID: {id}.");
                result.Exception = ex;
            }

            return result;
        }

        public async Task<Result<QueryResult<Customer>>> ListAsync(CustomerQueryParam queryParam)
        {
            var result = new Result<QueryResult<Customer>>();

            try
            {
              result.Data = await _customerRepository.ListAsync(queryParam);
              result.Sucess = true;
            }
            catch (Exception ex)
            {
                result.Errors.Add(Result.UnmanagedError, $"Erro não gerênciado ao consultar a lista de clientes. paramêtros: {queryParam.Serialize()}.");
                result.Exception = ex;
            }

            return result;
        }

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

        public async Task<Result> UpdateAsync(Customer customer)
        {
            var result = new Result();

            try
            {
                var validator = await new UpdateCustomerValidatorAsync(_customerRepository).ValidateAsync(customer);
                if (!validator.IsValid)
                {
                    result.Errors = validator.Errors.ToDictionary(v => v.PropertyName, v => v.ErrorMessage);
                    return result;
                }

                await _customerRepository.UpdateAsync(customer);

                result.Sucess = true;
            }
            catch (Exception ex)
            {
                result.Errors.Add(Result.UnmanagedError, "Erro não gerênciado ao atualizar os dados do cliente.");
                result.Exception = ex;
            }

            return result;
        }
    }
}