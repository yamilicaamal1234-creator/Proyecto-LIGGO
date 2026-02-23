using FluentValidation;

namespace Liggo.Application.UseCases.Billing.Customers.Commands.UpdateCustomer;

public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("El ID del Customer es obligatorio.");
        RuleFor(x => x.TenantId).GreaterThan(0).WithMessage("El ID del Tenant es obligatorio.");
        RuleFor(x => x.ExternalId).NotEmpty().WithMessage("El ExternalId no puede estar vacío.");
        RuleFor(x => x.BusinessName).NotEmpty().WithMessage("La razón social es obligatoria.");
        RuleFor(x => x.AdminEmail).NotEmpty().EmailAddress().WithMessage("Correo electrónico inválido.");
    }
}