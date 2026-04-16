using FluentValidation;
using MediatR;
using Liggo.Domain.Interfaces;
using Liggo.Application.Interfaces;

namespace Liggo.Application.Functions.Incidents.Commands
{
    public class UpdateIncidentCommand : IRequest<bool>
    {
        public string Id { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? IncidentType { get; set; }
        public string? Severeity { get; set; }
        public bool? Closed { get; set; }
        public DateTime? Date { get; set; }
    }

    public class UpdateIncidentCommandHandler : IRequestHandler<UpdateIncidentCommand, bool>
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly ICurrentUserService _currentUserService;

        public UpdateIncidentCommandHandler(IIncidentRepository incidentRepository, ICurrentUserService currentUserService)
        {
            _incidentRepository = incidentRepository;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(UpdateIncidentCommand request, CancellationToken cancellationToken)
        {
            var schoolId = _currentUserService.SchoolId;
            
            var updates = new Dictionary<string, object>();

            var existingIncident = await _incidentRepository.GetByIdAsync(schoolId, request.Id, cancellationToken);

            if (existingIncident == null)
            {
                throw new Exception("Incidente no encontrado");
            }

            if (request.Description != null)
                updates["Description"] = request.Description;

            if (request.IncidentType != null)
                updates["IncidentType"] = request.IncidentType;

            if (request.Severeity != null)
                updates["Severeity"] = request.Severeity;

            if (request.Closed.HasValue)
                updates["Closed"] = request.Closed.Value;

            if (updates.Count == 0)
                throw new Exception("No se proporcionaron campos para actualizar");

            await _incidentRepository.UpdatePartialAsync(schoolId, request.Id, updates, cancellationToken);

            return true;
        }
    }
}