using System;
using MediatR;
using Liggo.Application.UseCases.Operations.Players.Dtos;

namespace Liggo.Application.UseCases.Operations.Players.Queries.GetPlayerById
{
    public record GetPlayerByIdQuery(Guid Id, Guid AdminId) : IRequest<PlayerDto>;
}