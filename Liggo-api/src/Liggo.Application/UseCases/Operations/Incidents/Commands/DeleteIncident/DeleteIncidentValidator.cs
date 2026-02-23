using FluentValidation;

namespace Liggo.Application.UseCases.Operations.Incidents.Commands.DeleteIncident;

public class DeleteIncidentValidator : AbstractValidator<DeleteIncidentCommand>
{
    public DeleteIncidentValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("El ID es obligatorio para eliminar el Incidente.");
    }
}