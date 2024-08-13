using Customize.Domain.Repositories;
using FluentValidation;

namespace Customize.Domain.Validators
{
    public class UpdateCustomerValidatorAsync : SaveCustomerValidator
    {
        public UpdateCustomerValidatorAsync(ICustomerRepository customerRepository)
        {
            RuleFor(c=> c.Id)
                .MustAsync(async (x, _) => (await customerRepository.FindAsync(x)) != null).WithMessage(c=> $"Cliente não foi encontrado. ID:{c.Id}");
        }
    }
}
