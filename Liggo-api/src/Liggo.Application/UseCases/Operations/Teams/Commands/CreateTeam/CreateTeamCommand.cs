using MediatR;
using Liggo.Application.UseCases.Operations.Teams.Dtos;

namespace Liggo.Application.UseCases.Operations.Teams.Commands.CreateTeam;

public record CreateTeamCommand(
    string Name,
    string Category,
    string Coach,
    string LogoUrl,
    TeamStatsDto StatsTeam) : IRequest<string>;