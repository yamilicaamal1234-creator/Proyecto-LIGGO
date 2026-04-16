using MediatR;
using Liggo.Domain.Interfaces;
using Liggo.Application.Interfaces;

namespace Liggo.Application.Functions.Incidents.Commands
{
    public class DeleteIncidentCommand : IRequest<bool>
    {
        public string Id { get; set; } = string.Empty;
    }

    public class DeleteIncidentCommandHandler : IRequestHandler<DeleteIncidentCommand, bool>
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteIncidentCommandHandler(IIncidentRepository incidentRepository, ICurrentUserService currentUserService)
        {
            _incidentRepository = incidentRepository;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(DeleteIncidentCommand request, CancellationToken cancellationToken)
        {
            var schoolId = _currentUserService.SchoolId;

            var existingIncident = await _incidentRepository.GetByIdAsync(schoolId, request.Id, cancellationToken);

            if (existingIncident == null)
            {
                throw new Exception("Incidente no encontrado");
            }

            await _incidentRepository.DeleteAsync(schoolId, request.Id, cancellationToken);

            return true;
        }
    }
}