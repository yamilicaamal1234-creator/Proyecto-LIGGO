using FluentValidation;

namespace Liggo.Application.UseCases.Operations.Players.Queries.GetAllPlayersBySchoolId;

public class GetAllPlayersBySchoolIdValidator : AbstractValidator<GetAllPlayersBySchoolIdQuery>
{
    public GetAllPlayersBySchoolIdValidator()
    {
        RuleFor(x => x.SchoolId).NotEmpty().WithMessage("El ID de la escuela es obligatorio.");
    }
}