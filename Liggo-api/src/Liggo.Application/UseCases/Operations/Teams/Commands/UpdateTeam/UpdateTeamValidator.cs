using FluentValidation;

namespace Liggo.Application.UseCases.Operations.Teams.Commands.UpdateTeam;

public class UpdateTeamValidator : AbstractValidator<UpdateTeamCommand>
{
    public UpdateTeamValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("El ID del equipo es obligatorio.");
        RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre del equipo es obligatorio.");
        RuleFor(x => x.Category).NotEmpty().WithMessage("La categoría es obligatoria.");
        RuleFor(x => x.StatsTeam).NotNull();
    }
}