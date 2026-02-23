using Liggo.Application.UseCases.Operations.Teams.Dtos;

namespace Liggo.Application.UseCases.Operations.Teams.Queries.Responses;

public record TeamResponse(
    string Id,
    string Name,
    string Category,
    string Coach,
    string LogoUrl,
    TeamStatsDto StatsTeam);