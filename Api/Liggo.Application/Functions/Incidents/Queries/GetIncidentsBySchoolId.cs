using MediatR;
using Liggo.Application.DTOs;
using Liggo.Domain.Interfaces;
using Liggo.Application.Interfaces;

namespace Liggo.Application.Functions.Incidents.Queries
{
    public class GetIncidentsBySchoolIdQuery : IRequest<IEnumerable<IncidentDto>>
    {
        public string? Type { get; set; }
    }

    public class GetIncidentsBySchoolIdQueryHandler : IRequestHandler<GetIncidentsBySchoolIdQuery, IEnumerable<IncidentDto>>
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetIncidentsBySchoolIdQueryHandler(IIncidentRepository incidentRepository, ICurrentUserService currentUserService)
        {
            _incidentRepository = incidentRepository;
            _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<IncidentDto>> Handle(GetIncidentsBySchoolIdQuery request, CancellationToken cancellationToken)
        {
            var schoolId = _currentUserService.SchoolId;

            // 1. EL FILTRO VIAJA A LA BASE DE DATOS
            // Le pasamos request.Type al repositorio. Firebase hará el trabajo pesado.
            var incidents = await _incidentRepository.GetAllBySchoolAsync(schoolId, request.Type, cancellationToken);

            // 2. MAPEO ULTRARRÁPIDO EN MEMORIA (Sin N+1)
            var incidentDtos = incidents.Select(incident => new IncidentDto
            {
                Id = incident.Id,
                PlayerName = incident.PlayerName, // Lo leemos directo del documento denormalizado
                Description = incident.Description,
                Type = incident.IncidentType,
                Severity = incident.Severeity,
                Closed = incident.Closed,
                Date = incident.Date
            }).ToList();

            return incidentDtos;
        }
    }
}