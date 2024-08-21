using Customize.Domain.DataObject.Validators;
using Customize.Domain.Repositories;
using FluentValidation;

namespace Customize.Domain.Validators
{
    public class DeleteCustomerValidatorAsync : AbstractValidator<DeleteValidatorParam>
    {
        public DeleteCustomerValidatorAsync(ICustomerRepository customerRepository)
        {
            RuleFor(param => param.Id)
                .MustAsync(async (id, _) => (await customerRepository.FindAsync(id)) != null).WithMessage(param => $"Cliente não foi encontrado. ID:{param.Id}");
        }
    }
}
