using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Application.UseCases.Operations.Incidents.Commands.CreateIncident;

namespace Liggo.Application.UseCases.Operations.Incidents.Queries.GetIncidentById;

public class GetIncidentByIdHandler : IRequestHandler<GetIncidentByIdQuery, IncidentResponse?>
{
    private readonly IIncidentRepository _incidentRepository;

    public GetIncidentByIdHandler(IIncidentRepository incidentRepository)
    {
        _incidentRepository = incidentRepository;
    }

    public async Task<IncidentResponse?> Handle(GetIncidentByIdQuery request, CancellationToken cancellationToken)
    {
        var incident = await _incidentRepository.GetByIdAsync(request.Id, cancellationToken);

        if (incident == null) return null;

        return new IncidentResponse(
            incident.Id,
            incident.Type,
            incident.Severity,
            new IncidentContextDto(incident.Context.Student, incident.Context.Event),
            incident.Description,
            incident.Status
        );
    }
}