using FluentValidation;

namespace Liggo.Application.UseCases.Billing.Subscriptions.Commands.UpdateSubscription;

public class UpdateSubscriptionValidator : AbstractValidator<UpdateSubscriptionCommand>
{
    public UpdateSubscriptionValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("El ID de la Suscripción es obligatorio.");
        RuleFor(x => x.CustomerId).GreaterThan(0).WithMessage("El ID del Customer es obligatorio.");
        RuleFor(x => x.PlanId).GreaterThan(0).WithMessage("El ID del Plan es obligatorio.");
        RuleFor(x => x.Status).IsInEnum().WithMessage("Estado inválido.");
        RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate).WithMessage("La fecha de fin debe ser posterior a la de inicio.");
    }
}