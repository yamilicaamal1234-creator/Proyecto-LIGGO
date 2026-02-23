using MediatR;
using Liggo.Application.UseCases.Operations.Incidents.Commands.CreateIncident;

namespace Liggo.Application.UseCases.Operations.Incidents.Queries.GetIncidentById;

public record IncidentResponse(
    string Id,
    string Type,
    string Severity,
    IncidentContextDto Context,
    string Description,
    string Status);

public record GetIncidentByIdQuery(string Id) : IRequest<IncidentResponse?>;