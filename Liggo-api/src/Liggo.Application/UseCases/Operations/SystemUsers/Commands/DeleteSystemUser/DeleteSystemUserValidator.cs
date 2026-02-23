using FluentValidation;

namespace Liggo.Application.UseCases.Operations.SystemUsers.Commands.DeleteSystemUser;

public class DeleteSystemUserValidator : AbstractValidator<DeleteSystemUserCommand>
{
    public DeleteSystemUserValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("El ID es obligatorio para eliminar el usuario.");
    }
}