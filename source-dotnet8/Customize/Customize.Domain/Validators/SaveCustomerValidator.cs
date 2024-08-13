using Customize.Domain.Entities;
using FluentValidation;

namespace Customize.Domain.Validators
{
    public class SaveCustomerValidator : AbstractValidator<Customer>
    {
        public SaveCustomerValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id do cliente é obrigatório.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nome do cliente é obrigatório.");
            RuleFor(x => x.Cellphone)
                .NotEmpty().WithMessage("Número do celular é obrigatório")
                 .Matches("^(?:\\D*\\d){13}\\D*$").WithMessage("Número do celular inválido. Pais, DD, e numero são obrigatórios.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email inválido");
            RuleFor(x => x.CreatedAt).NotEqual(DateTime.MinValue).WithMessage("Data do cadastro inválido.");
        }
    }
}
