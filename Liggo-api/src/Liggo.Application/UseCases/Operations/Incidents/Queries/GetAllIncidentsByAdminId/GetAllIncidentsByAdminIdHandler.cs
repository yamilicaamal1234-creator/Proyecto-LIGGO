using System.Collections.Generic;
using System.Linq;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Application.UseCases.Operations.Incidents.Commands.CreateIncident; // Para el IncidentContextDto
using Liggo.Application.UseCases.Operations.Incidents.Queries.GetIncidentById; // Para el IncidentResponse

namespace Liggo.Application.UseCases.Operations.Incidents.Queries.GetAllIncidentsByAdminId;

public class GetAllIncidentsByAdminIdHandler : IRequestHandler<GetAllIncidentsByAdminIdQuery, IEnumerable<IncidentResponse>>
{
    private readonly IIncidentRepository _incidentRepository;

    public GetAllIncidentsByAdminIdHandler(IIncidentRepository incidentRepository)
    {
        _incidentRepository = incidentRepository;
    }

    public async Task<IEnumerable<IncidentResponse>> Handle(GetAllIncidentsByAdminIdQuery request, CancellationToken cancellationToken)
    {
        var incidents = await _incidentRepository.GetAllByAdminIdAsync(request.AdminId);

        if (incidents == null || !incidents.Any())
            return Enumerable.Empty<IncidentResponse>();

        return incidents.Select(incident => new IncidentResponse(
            incident.Id,
            incident.Type.ToString(),
            incident.Severity,
            new IncidentContextDto(incident.Context.Student, incident.Context.Event),
            incident.Description,
            incident.Status.ToString()
        ));
    }
}