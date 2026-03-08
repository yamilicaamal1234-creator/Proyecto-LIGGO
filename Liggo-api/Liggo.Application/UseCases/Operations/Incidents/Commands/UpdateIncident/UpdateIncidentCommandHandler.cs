using Liggo.Application.Interfaces;
using Liggo.Domain.Entities.Operations;
using MediatR;

namespace Liggo.Application.UseCases.Operations.Incidents.Commands.UpdateIncident;

public class UpdateIncidentCommandHandler : IRequestHandler<UpdateIncidentCommand>
{
    private readonly IIncidentRepository _incidentRepository;

    public UpdateIncidentCommandHandler(IIncidentRepository incidentRepository)
    {
        _incidentRepository = incidentRepository;
    }

    public async Task Handle(UpdateIncidentCommand request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.Id, out var id))
        {
            throw new ArgumentException("Invalid incident ID format.");
        }

        var incident = await _incidentRepository.GetByIdAsync(id);
        if (incident == null)
        {
            throw new KeyNotFoundException($"Incident with ID {request.Id} not found.");
        }

        incident.Type = request.Type;
        incident.Severity = Enum.Parse<IncidentSeverity>(request.Severity);
        incident.Context.PlayerId = request.Context.Student;
        incident.Context.MatchId = request.Context.Event;
        incident.Description = request.Description;
        incident.Status = Enum.Parse<IncidentStatus>(request.Status);

        await _incidentRepository.UpdateAsync(incident);
    }
}