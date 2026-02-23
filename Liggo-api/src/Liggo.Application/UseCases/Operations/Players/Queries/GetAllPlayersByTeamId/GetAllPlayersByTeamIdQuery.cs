using System.Collections.Generic;
using MediatR;
using Liggo.Application.UseCases.Operations.Players.Queries.Responses;

namespace Liggo.Application.UseCases.Operations.Players.Queries.GetAllPlayersByTeamId;

public record GetAllPlayersByTeamIdQuery(string TeamId) : IRequest<IEnumerable<PlayerResponse>>;