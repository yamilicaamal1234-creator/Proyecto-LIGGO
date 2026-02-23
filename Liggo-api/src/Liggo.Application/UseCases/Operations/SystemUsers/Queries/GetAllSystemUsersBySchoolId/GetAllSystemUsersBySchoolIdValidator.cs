using FluentValidation;

namespace Liggo.Application.UseCases.Operations.SystemUsers.Queries.GetAllSystemUsersBySchoolId;

public class GetAllSystemUsersBySchoolIdValidator : AbstractValidator<GetAllSystemUsersBySchoolIdQuery>
{
    public GetAllSystemUsersBySchoolIdValidator()
    {
        RuleFor(x => x.SchoolId).NotEmpty().WithMessage("El ID de la escuela es obligatorio.");
    }
}