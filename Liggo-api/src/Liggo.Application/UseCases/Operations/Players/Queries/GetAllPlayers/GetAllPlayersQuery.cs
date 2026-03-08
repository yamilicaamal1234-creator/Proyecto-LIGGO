using System;
using System.Collections.Generic;
using MediatR;
using Liggo.Application.UseCases.Operations.Players.Dtos;

namespace Liggo.Application.UseCases.Operations.Players.Queries.GetAllPlayers
{
    public record GetAllPlayersQuery(Guid AdminId) : IRequest<IEnumerable<PlayerDto>>;
}
