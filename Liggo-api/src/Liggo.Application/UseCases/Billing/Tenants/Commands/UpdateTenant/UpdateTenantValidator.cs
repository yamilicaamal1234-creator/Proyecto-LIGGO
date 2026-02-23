using FluentValidation;

namespace Liggo.Application.UseCases.Billing.Tenants.Commands.UpdateTenant;

public class UpdateTenantValidator : AbstractValidator<UpdateTenantCommand>
{
    public UpdateTenantValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("El ID del Tenant debe ser mayor a cero.");
        RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es obligatorio.");
        RuleFor(x => x.ApiKey).NotEmpty().WithMessage("El ApiKey es obligatorio.");
    }
}