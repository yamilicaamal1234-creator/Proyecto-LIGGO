using FluentValidation;

namespace Liggo.Application.UseCases.Billing.Subscriptions.Commands.CreateSubscription;

public class CreateSubscriptionValidator : AbstractValidator<CreateSubscriptionCommand>
{
    public CreateSubscriptionValidator()
    {
        RuleFor(x => x.CustomerId).GreaterThan(0).WithMessage("El ID del Customer es obligatorio.");
        RuleFor(x => x.PlanId).GreaterThan(0).WithMessage("El ID del Plan es obligatorio.");
        RuleFor(x => x.Status).IsInEnum().WithMessage("El estado de la suscripción no es válido.");
        
        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("La fecha de inicio es obligatoria.");
            
        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("La fecha de fin es obligatoria.")
            .GreaterThan(x => x.StartDate).WithMessage("La fecha de fin debe ser posterior a la fecha de inicio.");
    }
}