using System.Collections.Generic;
using System.Linq;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Application.UseCases.Operations.Incidents.Commands.CreateIncident;
using Liggo.Application.UseCases.Operations.Incidents.Queries.GetIncidentById;

namespace Liggo.Application.UseCases.Operations.Incidents.Queries.GetAllIncidentsByPlayerId;

public class GetAllIncidentsByPlayerIdHandler : IRequestHandler<GetAllIncidentsByPlayerIdQuery, IEnumerable<IncidentResponse>>
{
    private readonly IIncidentRepository _incidentRepository;

    public GetAllIncidentsByPlayerIdHandler(IIncidentRepository incidentRepository)
    {
        _incidentRepository = incidentRepository;
    }

    public async Task<IEnumerable<IncidentResponse>> Handle(GetAllIncidentsByPlayerIdQuery request, CancellationToken cancellationToken)
    {
        var incidents = await _incidentRepository.GetAllByPlayerIdAsync(request.PlayerId, cancellationToken);

        if (incidents == null || !incidents.Any())
            return Enumerable.Empty<IncidentResponse>();

        return incidents.Select(incident => new IncidentResponse(
            incident.Id,
            incident.Type,
            incident.Severity,
            new IncidentContextDto(incident.Context.Student, incident.Context.Event),
            incident.Description,
            incident.Status
        ));
    }
}