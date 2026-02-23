using FluentValidation;

namespace Liggo.Application.UseCases.Billing.Tenants.Queries.GetTenantById;

public class GetTenantByIdValidator : AbstractValidator<GetTenantByIdQuery>
{
    public GetTenantByIdValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("El ID es obligatorio para la búsqueda.");
    }
}