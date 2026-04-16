using FluentValidation;
using MediatR;
using Liggo.Domain.Entities.Documents;
using Liggo.Domain.Interfaces;
using Liggo.Application.Interfaces;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Data;

namespace Liggo.Application.Functions.Incidents.Commands
{
    public class CreateIncidentCommand : IRequest<string>
    {
        public string Type { get; set; } = string.Empty;
        public string PlayerId { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string IncidentType { get; set; } = string.Empty;
        public string Severeity { get; set; } = string.Empty;
    }

    public class CreateIncidentCommandValidator : AbstractValidator<CreateIncidentCommand>
    {
        public CreateIncidentCommandValidator()
        {
            RuleFor(x => x.Type).NotEmpty().WithMessage("El tipo de incidente es obligatorio");
            RuleFor(x => x.PlayerId).NotEmpty().WithMessage("El jugador es obligatorio");
            RuleFor(x => x.Description).NotEmpty().WithMessage("La descripción es obligatoria");
            RuleFor(x => x.IncidentType).NotEmpty().WithMessage("El tipo de incidente es obligatorio");
            RuleFor(x => x.Severeity).NotEmpty().WithMessage("La severidad es obligatoria");
        }
    }

    public class CreateIncidentCommandHandler : IRequestHandler<CreateIncidentCommand, string>
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly ICurrentUserService _currentUserService;

        public CreateIncidentCommandHandler(IIncidentRepository incidentRepository, ICurrentUserService currentUserService)
        {
            _incidentRepository = incidentRepository;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(CreateIncidentCommand request, CancellationToken cancellationToken)
        {
            var schoolId = _currentUserService.SchoolId;

            var newIncident = new Incident
            {
                Id = Guid.NewGuid().ToString(),
                PlayerId = request.PlayerId,
                Description = request.Description,
                IncidentType = request.Type,
                Severeity = request.Severeity,
                Closed = false,
                Date = DateTime.UtcNow
            };

            await _incidentRepository.AddAsync(schoolId, newIncident, cancellationToken);

            return newIncident.Id;
        }
    }
}