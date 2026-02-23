using FluentValidation;

namespace Liggo.Application.UseCases.Billing.Plans.Commands.CreatePlan;

public class CreatePlanValidator : AbstractValidator<CreatePlanCommand>
{
    public CreatePlanValidator()
    {
        RuleFor(x => x.TenantId).GreaterThan(0).WithMessage("El ID del Tenant es obligatorio.");
        
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del plan es obligatorio.");
            
        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("El precio no puede ser negativo.");
            
        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("La moneda es obligatoria.")
            .Length(3).WithMessage("La moneda debe tener exactamente 3 caracteres (ej. MXN, USD).");
            
        RuleFor(x => x.Frequency)
            .IsInEnum().WithMessage("La frecuencia seleccionada no es válida.");
    }
}