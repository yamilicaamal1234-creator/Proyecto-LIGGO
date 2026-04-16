using MediatR;
using Liggo.Application.DTOs;
using Liggo.Domain.Interfaces;
using Liggo.Application.Interfaces;

namespace Liggo.Domain.Functions.Incidents.Queries
{
    public class GetIncidentByIdQuery : IRequest<IncidentDto>
    {
        public string IncidentId { get; set; } = string.Empty;
    }

    public class GetIncidentByIdQueryHandler : IRequestHandler<GetIncidentByIdQuery, IncidentDto>
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetIncidentByIdQueryHandler(IIncidentRepository incidentRepository, ICurrentUserService currentUserService)
        {
            _incidentRepository = incidentRepository;
            _currentUserService = currentUserService;
        }

        public async Task<IncidentDto> Handle(GetIncidentByIdQuery request, CancellationToken cancellationToken)
        {
            var schoolId = _currentUserService.SchoolId;

            var incident = await _incidentRepository.GetByIdAsync(schoolId, request.IncidentId, cancellationToken);

            if (incident == null)
            {
                throw new Exception("Incidente no encontrado");
            }


            var dto = new IncidentDto
            {
                Id = incident.Id,
                PlayerName = incident.PlayerName,
                Description = incident.Description,
                Type = incident.IncidentType,
                Severity = incident.Severeity,
                Closed = incident.Closed,
                Date = incident.Date
            };
            return dto;
        }
    }
}