using System;
using MediatR;

namespace Liggo.Application.UseCases.Operations.Players.Commands.DeletePlayer
{
    public record DeletePlayerCommand(Guid Id, Guid AdminId) : IRequest<Unit>;
}