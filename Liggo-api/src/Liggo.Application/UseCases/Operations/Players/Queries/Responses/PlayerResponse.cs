using System.Collections.Generic;
using Liggo.Application.UseCases.Operations.Players.Dtos;

namespace Liggo.Application.UseCases.Operations.Players.Queries.Responses;

public record PlayerResponse(
    string Id,
    PlayerInfoDto Info,
    string Status,
    string Team,
    List<string> Parents,
    PlayerStatsDto Stats);