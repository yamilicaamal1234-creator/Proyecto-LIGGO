using MediatR;

namespace Liggo.Application.UseCases.Operations.Incidents.Commands.DeleteIncident;

public record DeleteIncidentCommand(string Id) : IRequest;