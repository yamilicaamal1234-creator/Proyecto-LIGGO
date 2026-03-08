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
        var incident = await _incidentRepository.GetByIdAsync(Guid.Parse(request.Id), Guid.Empty);

        if (incident == null) return false;

        await _incidentRepository.DeleteAsync(Guid.Parse(request.Id), Guid.Empty);
        return true;
    }
}