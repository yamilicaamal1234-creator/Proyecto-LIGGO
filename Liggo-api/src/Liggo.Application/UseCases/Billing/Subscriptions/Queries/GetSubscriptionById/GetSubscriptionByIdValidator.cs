using FluentValidation;

namespace Liggo.Application.UseCases.Billing.Subscriptions.Queries.GetSubscriptionById;

public class GetSubscriptionByIdValidator : AbstractValidator<GetSubscriptionByIdQuery>
{
    public GetSubscriptionByIdValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("El ID es obligatorio para la búsqueda.");
    }
}