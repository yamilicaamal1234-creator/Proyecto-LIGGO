using FluentValidation;

namespace Liggo.Application.UseCases.Operations.Incidents.Commands.UpdateIncident;

public class UpdateIncidentValidator : AbstractValidator<UpdateIncidentCommand>
{
    public UpdateIncidentValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("El ID del incidente es obligatorio.");
        
        RuleFor(x => x.Type).Must(x => x == "injury" || x == "discipline").WithMessage("Tipo inválido.");
        RuleFor(x => x.Severity).Must(x => x == "low" || x == "medium" || x == "high").WithMessage("Severidad inválida.");
        RuleFor(x => x.Status).Must(x => x == "open" || x == "closed").WithMessage("Estado inválido.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("La descripción es obligatoria.");
        
        RuleFor(x => x.Context).NotNull();
        When(x => x.Context != null, () =>
        {
            RuleFor(x => x.Context.Student).NotEmpty().WithMessage("El alumno es obligatorio.");
        });
    }
}