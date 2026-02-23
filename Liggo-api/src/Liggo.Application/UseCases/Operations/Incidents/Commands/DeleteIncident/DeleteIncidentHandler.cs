using MediatR;
using Liggo.Application.Interfaces.Operations;

namespace Liggo.Application.UseCases.Operations.Incidents.Commands.DeleteIncident;

public class DeleteIncidentHandler : IRequestHandler<DeleteIncidentCommand, bool>
{
    private readonly IIncidentRepository _incidentRepository;

    public DeleteIncidentHandler(IIncidentRepository incidentRepository)
    {
        _incidentRepository = incidentRepository;
    }

    public async Task<bool> Handle(DeleteIncidentCommand request, CancellationToken cancellationToken)
    {
        var incident = await _incidentRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (incident == null) return false;

        await _incidentRepository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}