using MediatR;
using Liggo.Application.UseCases.Operations.Teams.Dtos;

namespace Liggo.Application.UseCases.Operations.Teams.Commands.UpdateTeam;

public record UpdateTeamCommand(
    string Id,
    string Name,
    string Category,
    string Coach,
    string LogoUrl,
    TeamStatsDto StatsTeam) : IRequest<bool>;