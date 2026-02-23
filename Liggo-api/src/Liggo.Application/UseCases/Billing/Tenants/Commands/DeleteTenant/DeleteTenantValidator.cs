using FluentValidation;

namespace Liggo.Application.UseCases.Billing.Tenants.Commands.DeleteTenant;

public class DeleteTenantValidator : AbstractValidator<DeleteTenantCommand>
{
    public DeleteTenantValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("El ID del Tenant es obligatorio para eliminarlo.");
    }
}