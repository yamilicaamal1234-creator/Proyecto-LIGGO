using FluentValidation;

namespace Liggo.Application.UseCases.Operations.Teams.Queries.GetTeamById;

public class GetTeamByIdValidator : AbstractValidator<GetTeamByIdQuery>
{
    public GetTeamByIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("El ID es obligatorio para la búsqueda.");
    }
}