using System.Collections.Generic;
using MediatR;
using Liggo.Application.UseCases.Operations.Players.Dtos;

namespace Liggo.Application.UseCases.Operations.Players.Commands.UpdatePlayer;

public record UpdatePlayerCommand(
    string Id,
    PlayerInfoDto Info,
    string Status,
    string Team,
    List<string> Parents,
    PlayerStatsDto Stats) : IRequest<bool>;