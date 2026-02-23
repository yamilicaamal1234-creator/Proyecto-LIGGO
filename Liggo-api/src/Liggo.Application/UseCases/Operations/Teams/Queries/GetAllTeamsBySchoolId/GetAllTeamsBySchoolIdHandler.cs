using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Application.UseCases.Operations.Teams.Dtos;
using Liggo.Application.UseCases.Operations.Teams.Queries.Responses;

namespace Liggo.Application.UseCases.Operations.Teams.Queries.GetAllTeamsBySchoolId;

public class GetAllTeamsBySchoolIdHandler : IRequestHandler<GetAllTeamsBySchoolIdQuery, IEnumerable<TeamResponse>>
{
    private readonly ITeamRepository _teamRepository;

    public GetAllTeamsBySchoolIdHandler(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<IEnumerable<TeamResponse>> Handle(GetAllTeamsBySchoolIdQuery request, CancellationToken cancellationToken)
    {
        var teams = await _teamRepository.GetAllBySchoolIdAsync(request.SchoolId, cancellationToken);
        
        if (teams == null || !teams.Any()) 
            return Enumerable.Empty<TeamResponse>();

        return teams.Select(team => new TeamResponse(
            team.Id,
            team.Name,
            team.Category,
            team.Coach,
            team.LogoUrl,
            new TeamStatsDto(team.StatsTeam.Won, team.StatsTeam.Lost, team.StatsTeam.Tied)
        ));
    }
}