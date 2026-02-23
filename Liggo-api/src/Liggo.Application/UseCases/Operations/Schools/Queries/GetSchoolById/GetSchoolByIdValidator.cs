using FluentValidation;

namespace Liggo.Application.UseCases.Operations.Schools.Queries.GetSchoolById;

public class GetSchoolByIdValidator : AbstractValidator<GetSchoolByIdQuery>
{
    public GetSchoolByIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("El ID es obligatorio para la búsqueda.");
    }
}