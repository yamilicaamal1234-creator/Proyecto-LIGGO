using System;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Domain.Entities.Operations;
using Liggo.Domain.Enums;

namespace Liggo.Application.UseCases.Operations.Incidents.Commands.CreateIncident;

public class CreateIncidentHandler : IRequestHandler<CreateIncidentCommand, Guid>
{
    private readonly IIncidentRepository _incidentRepository;

    public CreateIncidentHandler(IIncidentRepository incidentRepository)
    {
        _incidentRepository = incidentRepository;
    }

    public async Task<Guid> Handle(CreateIncidentCommand request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.PlayerId, out var playerId))
        {
            throw new ArgumentException("Invalid PlayerId format");
        }

        if (!Enum.TryParse<IncidentType>(request.Type, true, out var incidentType))
            incidentType = IncidentType.Administrative;

        if (!Enum.TryParse<IncidentStatus>(request.Status, true, out var incidentStatus))
            incidentStatus = IncidentStatus.Active;

        var incident = new Incident
        {
            Id = Guid.NewGuid(),
            AdminId = request.AdminId,
            PlayerId = playerId,
            Type = incidentType,
            Severity = request.Severity ?? "Moderate",
            Description = request.Description ?? "",
            Status = incidentStatus,
            Context = new IncidentContext
            {
                Student = request.Context?.Student ?? "",
                Event = request.Context?.Event ?? "Manual Report"
            }
        };

        await _incidentRepository.AddAsync(incident);

        return incident.Id;
    }
}