using FluentValidation;

namespace Liggo.Application.UseCases.Billing.Plans.Queries.GetPlanById;

public class GetPlanByIdValidator : AbstractValidator<GetPlanByIdQuery>
{
    public GetPlanByIdValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("El ID es obligatorio para la búsqueda.");
    }
}