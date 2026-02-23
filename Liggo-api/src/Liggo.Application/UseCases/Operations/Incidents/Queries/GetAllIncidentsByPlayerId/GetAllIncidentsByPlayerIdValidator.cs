using FluentValidation;

namespace Liggo.Application.UseCases.Operations.Incidents.Queries.GetAllIncidentsByPlayerId;

public class GetAllIncidentsByPlayerIdValidator : AbstractValidator<GetAllIncidentsByPlayerIdQuery>
{
    public GetAllIncidentsByPlayerIdValidator()
    {
        RuleFor(x => x.PlayerId).NotEmpty().WithMessage("El ID del jugador es obligatorio para ver su historial de incidentes.");
    }
}