using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Application.UseCases.Operations.Teams.Dtos;
using Liggo.Application.UseCases.Operations.Teams.Queries.Responses;

namespace Liggo.Application.UseCases.Operations.Teams.Queries.GetTeamById;

public class GetTeamByIdHandler : IRequestHandler<GetTeamByIdQuery, TeamResponse?>
{
    private readonly ITeamRepository _teamRepository;

    public GetTeamByIdHandler(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<TeamResponse?> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
    {
        var team = await _teamRepository.GetByIdAsync(request.Id, cancellationToken);
        if (team == null) return null;

        return new TeamResponse(
            team.Id,
            team.Name,
            team.Category,
            team.Coach,
            team.LogoUrl,
            new TeamStatsDto(team.StatsTeam.Won, team.StatsTeam.Lost, team.StatsTeam.Tied)
        );
    }
}