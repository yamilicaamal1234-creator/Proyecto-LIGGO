using FluentValidation;

namespace Liggo.Application.UseCases.Operations.Schools.Commands.DeleteSchool;

public class DeleteSchoolValidator : AbstractValidator<DeleteSchoolCommand>
{
    public DeleteSchoolValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("El ID es obligatorio para eliminar la escuela.");
    }
}