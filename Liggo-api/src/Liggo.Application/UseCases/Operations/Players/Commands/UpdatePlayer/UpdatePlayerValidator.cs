using FluentValidation;

namespace Liggo.Application.UseCases.Operations.Players.Commands.UpdatePlayer;

public class UpdatePlayerValidator : AbstractValidator<UpdatePlayerCommand>
{
    public UpdatePlayerValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("El ID del jugador es obligatorio.");
        RuleFor(x => x.Status).NotEmpty().WithMessage("El estado es obligatorio.");
        RuleFor(x => x.Team).NotEmpty().WithMessage("El ID del equipo es obligatorio.");

        RuleFor(x => x.Info).NotNull();
        When(x => x.Info != null, () => RuleFor(x => x.Info.Name).NotEmpty());

        RuleFor(x => x.Stats).NotNull();
    }
}