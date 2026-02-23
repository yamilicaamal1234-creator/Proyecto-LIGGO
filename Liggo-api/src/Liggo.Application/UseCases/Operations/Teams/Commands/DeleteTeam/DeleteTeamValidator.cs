using FluentValidation;

namespace Liggo.Application.UseCases.Operations.Teams.Commands.DeleteTeam;

public class DeleteTeamValidator : AbstractValidator<DeleteTeamCommand>
{
    public DeleteTeamValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("El ID es obligatorio para eliminar el equipo.");
    }
}