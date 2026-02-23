using MediatR;

namespace Liggo.Application.UseCases.Operations.Players.Commands.DeletePlayer;

public record DeletePlayerCommand(string Id) : IRequest<bool>;