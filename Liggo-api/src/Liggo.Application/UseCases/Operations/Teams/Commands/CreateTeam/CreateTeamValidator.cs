using FluentValidation;

namespace Liggo.Application.UseCases.Operations.Teams.Commands.CreateTeam;

public class CreateTeamValidator : AbstractValidator<CreateTeamCommand>
{
    public CreateTeamValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre del equipo es obligatorio.");
        RuleFor(x => x.Category).NotEmpty().WithMessage("La categoría es obligatoria.");
        
        RuleFor(x => x.StatsTeam).NotNull().WithMessage("Las estadísticas del equipo son obligatorias (pueden iniciar en cero).");
        When(x => x.StatsTeam != null, () =>
        {
            RuleFor(x => x.StatsTeam.Won).GreaterThanOrEqualTo(0);
            RuleFor(x => x.StatsTeam.Lost).GreaterThanOrEqualTo(0);
            RuleFor(x => x.StatsTeam.Tied).GreaterThanOrEqualTo(0);
        });
    }
}