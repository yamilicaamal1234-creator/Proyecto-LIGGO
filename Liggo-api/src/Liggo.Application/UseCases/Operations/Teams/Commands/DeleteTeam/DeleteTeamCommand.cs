using MediatR;

namespace Liggo.Application.UseCases.Operations.Teams.Commands.DeleteTeam;

public record DeleteTeamCommand(string Id) : IRequest<bool>;