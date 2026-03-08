using System;
using MediatR;

namespace Liggo.Application.UseCases.Operations.Matches.Commands.DeleteMatch
{
    public record DeleteMatchCommand(Guid Id, Guid AdminId) : IRequest<Unit>;
}
