using MediatR;
using Liggo.Application.UseCases.Operations.Players.Queries.Responses;

namespace Liggo.Application.UseCases.Operations.Players.Queries.GetPlayerById;

public record GetPlayerByIdQuery(string Id) : IRequest<PlayerResponse?>;