using FluentValidation;

namespace Liggo.Application.UseCases.Billing.Plans.Commands.UpdatePlan;

public class UpdatePlanValidator : AbstractValidator<UpdatePlanCommand>
{
    public UpdatePlanValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("El ID del Plan es obligatorio.");
        RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre del plan es obligatorio.");
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("El precio no puede ser negativo.");
        RuleFor(x => x.Currency).NotEmpty().Length(3).WithMessage("La moneda debe tener 3 caracteres.");
        RuleFor(x => x.Frequency).IsInEnum().WithMessage("Frecuencia inválida.");
    }
}