using FluentValidation;

namespace Liggo.Application.UseCases.Operations.Players.Commands.DeletePlayer;

public class DeletePlayerValidator : AbstractValidator<DeletePlayerCommand>
{
    public DeletePlayerValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("El ID es obligatorio para eliminar.");
    }
}