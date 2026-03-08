using Liggo.Application.Interfaces;
using MediatR;

namespace Liggo.Application.UseCases.Operations.Incidents.Commands.DeleteIncident;

public class DeleteIncidentCommandHandler : IRequestHandler<DeleteIncidentCommand>
{
    private readonly IIncidentRepository _incidentRepository;

    public DeleteIncidentCommandHandler(IIncidentRepository incidentRepository)
    {
        _incidentRepository = incidentRepository;
    }

    public async Task Handle(DeleteIncidentCommand request, CancellationToken cancellationToken)
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

        await _incidentRepository.DeleteAsync(incident);
    }
}