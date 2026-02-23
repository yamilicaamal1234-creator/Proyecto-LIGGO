using FluentValidation;

namespace Liggo.Application.UseCases.Operations.Members.Commands.DeleteMember;

public class DeleteMemberValidator : AbstractValidator<DeleteMemberCommand>
{
    public DeleteMemberValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("El ID es obligatorio para eliminar.");
    }
}