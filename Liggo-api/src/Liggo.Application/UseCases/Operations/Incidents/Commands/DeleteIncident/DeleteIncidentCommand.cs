using MediatR;

public record DeleteIncidentCommand(string Id) : IRequest<bool>;