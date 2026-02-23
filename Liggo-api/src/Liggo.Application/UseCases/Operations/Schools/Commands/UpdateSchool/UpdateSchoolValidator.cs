using FluentValidation;

namespace Liggo.Application.UseCases.Operations.Schools.Commands.UpdateSchool;

public class UpdateSchoolValidator : AbstractValidator<UpdateSchoolCommand>
{
    public UpdateSchoolValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("El ID de la escuela es obligatorio.");
        
        RuleFor(x => x.Info).NotNull();
        When(x => x.Info != null, () => RuleFor(x => x.Info.Name).NotEmpty());

        RuleFor(x => x.Settings).NotNull();
    }
}