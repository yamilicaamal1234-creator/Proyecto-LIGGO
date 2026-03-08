using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Domain.Enums;

namespace Liggo.Application.UseCases.Operations.Incidents.Commands.UpdateIncident;

public class UpdateIncidentHandler : IRequestHandler<UpdateIncidentCommand, bool>
{
    private readonly IIncidentRepository _incidentRepository;

    public UpdateIncidentHandler(IIncidentRepository incidentRepository)
    {
        _incidentRepository = incidentRepository;
    }

    public async Task<bool> Handle(UpdateIncidentCommand request, CancellationToken cancellationToken)
    {
        var incident = await _incidentRepository.GetByIdAsync(Guid.Parse(request.Id), Guid.Empty); // AdminId not used in GetById

        if (incident == null) return false;

        incident.Type = Enum.Parse<IncidentType>(request.Type);
        incident.Severity = request.Severity;
        incident.Description = request.Description;
        incident.Status = Enum.Parse<IncidentStatus>(request.Status);

        incident.Context.Student = request.Context.Student;
        incident.Context.Event = request.Context.Event;

        await _incidentRepository.UpdateAsync(incident);

        return true;
    }
}