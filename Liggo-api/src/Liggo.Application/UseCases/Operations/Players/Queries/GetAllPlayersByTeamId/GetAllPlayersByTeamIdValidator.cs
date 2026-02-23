using FluentValidation;

namespace Liggo.Application.UseCases.Operations.Players.Queries.GetAllPlayersByTeamId;

public class GetAllPlayersByTeamIdValidator : AbstractValidator<GetAllPlayersByTeamIdQuery>
{
    public GetAllPlayersByTeamIdValidator()
    {
        RuleFor(x => x.TeamId).NotEmpty().WithMessage("El ID del equipo es obligatorio.");
    }
}