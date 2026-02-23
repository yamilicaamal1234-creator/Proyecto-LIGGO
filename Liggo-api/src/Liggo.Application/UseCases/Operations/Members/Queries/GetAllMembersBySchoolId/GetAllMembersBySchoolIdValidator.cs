using FluentValidation;

namespace Liggo.Application.UseCases.Operations.Members.Queries.GetAllMembersBySchoolId;

public class GetAllMembersBySchoolIdValidator : AbstractValidator<GetAllMembersBySchoolIdQuery>
{
    public GetAllMembersBySchoolIdValidator()
    {
        RuleFor(x => x.SchoolId).NotEmpty().WithMessage("El ID de la escuela es obligatorio.");
    }
}