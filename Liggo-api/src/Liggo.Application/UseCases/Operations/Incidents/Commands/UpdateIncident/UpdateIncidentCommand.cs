using MediatR;
using Liggo.Application.UseCases.Operations.Incidents.Commands.CreateIncident; // Reusamos el DTO

namespace Liggo.Application.UseCases.Operations.Incidents.Commands.UpdateIncident;

public record UpdateIncidentCommand(
    string Id,
    string Type,
    string Severity,
    IncidentContextDto Context,
    string Description,
    string Status) : IRequest<bool>;