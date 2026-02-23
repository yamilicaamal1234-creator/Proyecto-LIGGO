using FluentValidation;

namespace Liggo.Application.UseCases.Billing.Subscriptions.Commands.DeleteSubscription;

public class DeleteSubscriptionValidator : AbstractValidator<DeleteSubscriptionCommand>
{
    public DeleteSubscriptionValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("El ID es obligatorio para eliminar la Suscripción.");
    }
}