using MediatR;
using Liggo.Application.UseCases.Operations.Incidents.Commands.CreateIncident;

namespace Liggo.Application.UseCases.Operations.Incidents.Commands.UpdateIncident;

public record UpdateIncidentCommand(
    Guid Id,
    string Type,
    string Severity,
    IncidentContextDto Context,
    string Description,
    string Status) : IRequest;