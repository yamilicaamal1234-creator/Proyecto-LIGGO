using FluentValidation;

namespace Liggo.Application.UseCases.Operations.Incidents.Commands.CreateIncident;

public class CreateIncidentValidator : AbstractValidator<CreateIncidentCommand>
{
    public CreateIncidentValidator()
    {
        RuleFor(x => x.Type)
            .Must(x => x == "injury" || x == "discipline")
            .WithMessage("El tipo de incidente solo puede ser 'injury' o 'discipline'.");

        RuleFor(x => x.Severity)
            .Must(x => x == "low" || x == "medium" || x == "high")
            .WithMessage("La severidad debe ser 'low', 'medium' o 'high'.");

        RuleFor(x => x.Status)
            .Must(x => x == "open" || x == "closed")
            .WithMessage("El estado debe ser 'open' o 'closed'.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("La descripción del incidente es obligatoria.");

        RuleFor(x => x.Context).NotNull().WithMessage("El contexto del incidente es obligatorio.");
        When(x => x.Context != null, () =>
        {
            RuleFor(x => x.Context.Student).NotEmpty().WithMessage("El alumno (Student) es obligatorio en el contexto.");
            // Event podría ser opcional según tu lógica, si es obligatorio, descomenta la siguiente línea:
            // RuleFor(x => x.Context.Event).NotEmpty().WithMessage("El evento es obligatorio en el contexto.");
        });
    }
}