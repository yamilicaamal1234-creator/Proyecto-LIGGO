using System.Collections.Generic;
using System.Linq;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Application.UseCases.Operations.Incidents.Commands.CreateIncident; // Para el IncidentContextDto
using Liggo.Application.UseCases.Operations.Incidents.Queries.GetIncidentById; // Para el IncidentResponse

namespace Liggo.Application.UseCases.Operations.Incidents.Queries.GetAllIncidentsBySchoolId;

public class GetAllIncidentsBySchoolIdHandler : IRequestHandler<GetAllIncidentsBySchoolIdQuery, IEnumerable<IncidentResponse>>
{
    private readonly IIncidentRepository _incidentRepository;

    public GetAllIncidentsBySchoolIdHandler(IIncidentRepository incidentRepository)
    {
        _incidentRepository = incidentRepository;
    }

    public async Task<IEnumerable<IncidentResponse>> Handle(GetAllIncidentsBySchoolIdQuery request, CancellationToken cancellationToken)
    {
        var incidents = await _incidentRepository.GetAllBySchoolIdAsync(request.SchoolId, cancellationToken);

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