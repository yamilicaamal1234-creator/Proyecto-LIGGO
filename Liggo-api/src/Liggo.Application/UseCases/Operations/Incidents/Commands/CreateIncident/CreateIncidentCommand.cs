using MediatR;

namespace Liggo.Application.UseCases.Operations.Incidents.Commands.CreateIncident;

// DTO para el contexto anidado
public record IncidentContextDto(string Student, string Event);

public record CreateIncidentCommand(
    string Type,
    string Severity,
    IncidentContextDto Context,
    string Description,
    string Status) : IRequest<string>; // Retorna el string ID