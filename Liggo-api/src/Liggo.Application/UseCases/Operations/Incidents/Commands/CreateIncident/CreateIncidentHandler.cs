using System;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Domain.Entities.Operations;

namespace Liggo.Application.UseCases.Operations.Incidents.Commands.CreateIncident;

public class CreateIncidentHandler : IRequestHandler<CreateIncidentCommand, string>
{
    private readonly IIncidentRepository _incidentRepository;

    public CreateIncidentHandler(IIncidentRepository incidentRepository)
    {
        _incidentRepository = incidentRepository;
    }

    public async Task<string> Handle(CreateIncidentCommand request, CancellationToken cancellationToken)
    {
        var incident = new Incident
        {
            Id = Guid.NewGuid().ToString(), // Generamos el ID para Firebase
            Type = request.Type,
            Severity = request.Severity,
            Description = request.Description,
            Status = request.Status,
            Context = new IncidentContext
            {
                Student = request.Context.Student,
                Event = request.Context.Event
            }
        };

        await _incidentRepository.AddAsync(incident, cancellationToken);

        return incident.Id;
    }
}