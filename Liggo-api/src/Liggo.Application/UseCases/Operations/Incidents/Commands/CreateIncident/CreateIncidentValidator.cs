using FluentValidation;

namespace Liggo.Application.UseCases.Operations.Incidents.Commands.CreateIncident;

public class CreateIncidentValidator : AbstractValidator<CreateIncidentCommand>
{
    public CreateIncidentValidator()
    {
        RuleFor(x => x.PlayerId)
            .NotEmpty().WithMessage("El jugador es obligatorio.");

        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("El tipo de incidente es obligatorio.");

        RuleFor(x => x.Severity)
            .NotEmpty().WithMessage("La severidad es obligatoria.");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("El estado es obligatorio.");

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