using MediatR;
using Liggo.Application.UseCases.Operations.Incidents.Commands.CreateIncident;

namespace Liggo.Application.UseCases.Operations.Incidents.Queries.GetIncidentById;

public record IncidentResponse(
    Guid Id,
    string Type,
    string Severity,
    IncidentContextDto Context,
    string Description,
    string Status);

public record GetIncidentByIdQuery(Guid Id) : IRequest<IncidentResponse?>;