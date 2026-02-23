using FluentValidation;

namespace Liggo.Application.UseCases.Operations.Teams.Queries.GetAllTeamsBySchoolId;

public class GetAllTeamsBySchoolIdValidator : AbstractValidator<GetAllTeamsBySchoolIdQuery>
{
    public GetAllTeamsBySchoolIdValidator()
    {
        RuleFor(x => x.SchoolId).NotEmpty().WithMessage("El ID de la escuela es obligatorio.");
    }
}