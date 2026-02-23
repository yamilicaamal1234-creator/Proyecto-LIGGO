using FluentValidation;

namespace Liggo.Application.UseCases.Operations.Schools.Commands.CreateSchool;

public class CreateSchoolValidator : AbstractValidator<CreateSchoolCommand>
{
    public CreateSchoolValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("El ID de la escuela es obligatorio (debe coincidir con ExternalId en MySQL).");
        
        RuleFor(x => x.Info).NotNull().WithMessage("La información de la escuela es obligatoria.");
        When(x => x.Info != null, () =>
        {
            RuleFor(x => x.Info.Name).NotEmpty().WithMessage("El nombre de la escuela es obligatorio.");
            RuleFor(x => x.Info.Plan).NotEmpty().WithMessage("El plan asignado es obligatorio.");
        });

        RuleFor(x => x.Settings).NotNull().WithMessage("Las configuraciones de la escuela son obligatorias.");
        When(x => x.Settings != null, () =>
        {
            RuleFor(x => x.Settings.Currency).NotEmpty().WithMessage("La moneda (Currency) es obligatoria.");
            RuleFor(x => x.Settings.Timezone).NotEmpty().WithMessage("La zona horaria es obligatoria.");
        });
    }
}