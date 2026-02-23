using FluentValidation;

namespace Liggo.Application.UseCases.Operations.Players.Commands.CreatePlayer;

public class CreatePlayerValidator : AbstractValidator<CreatePlayerCommand>
{
    public CreatePlayerValidator()
    {
        RuleFor(x => x.Status).NotEmpty().WithMessage("El estado es obligatorio.");
        RuleFor(x => x.Team).NotEmpty().WithMessage("El ID del equipo es obligatorio.");
        
        RuleFor(x => x.Info).NotNull().WithMessage("La información del jugador es obligatoria.");
        When(x => x.Info != null, () =>
        {
            RuleFor(x => x.Info.Name).NotEmpty().WithMessage("El nombre del jugador es obligatorio.");
            RuleFor(x => x.Info.Dob).NotEmpty().WithMessage("La fecha de nacimiento es obligatoria.");
        });

        RuleFor(x => x.Stats).NotNull().WithMessage("Las estadísticas son obligatorias (pueden ir en cero).");
    }
}