using MediatR;
using Liggo.Application.Interfaces.Operations;

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
        var incident = await _incidentRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (incident == null) return false;

        incident.Type = request.Type;
        incident.Severity = request.Severity;
        incident.Description = request.Description;
        incident.Status = request.Status;
        
        incident.Context.Student = request.Context.Student;
        incident.Context.Event = request.Context.Event;

        await _incidentRepository.UpdateAsync(incident, cancellationToken);
        
        return true;
    }
}