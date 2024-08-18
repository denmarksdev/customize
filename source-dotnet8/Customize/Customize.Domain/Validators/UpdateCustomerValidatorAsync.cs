using Customize.Domain.Repositories;
using FluentValidation;

namespace Customize.Domain.Validators
{
    public class UpdateCustomerValidatorAsync : SaveCustomerValidator
    {
        public DateTime? CreatedAt { get; set; }

        public UpdateCustomerValidatorAsync(ICustomerRepository customerRepository):base(ignoreCreatedAt:true)
        {
            RuleFor(c => c.Id)
                .MustAsync(async (x, _) => await ValidateCustomer(customerRepository, x)).WithMessage(c => $"Cliente não foi encontrado. ID:{c.Id}");
        }

        private async Task<bool> ValidateCustomer(ICustomerRepository customerRepository, string x)
        {
            var customer = await customerRepository.FindAsync(x);

            if (customer != null) 
            {
                CreatedAt = customer.CreatedAt;
            }

            return customer != null;
        }
    }
}
