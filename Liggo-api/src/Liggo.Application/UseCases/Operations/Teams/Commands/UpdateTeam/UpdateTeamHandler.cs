using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;

namespace Liggo.Application.UseCases.Operations.Teams.Commands.UpdateTeam;

public class UpdateTeamHandler : IRequestHandler<UpdateTeamCommand, bool>
{
    private readonly ITeamRepository _teamRepository;

    public UpdateTeamHandler(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<bool> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
    {
        var team = await _teamRepository.GetByIdAsync(request.Id, cancellationToken);
        if (team == null) return false;

        team.Name = request.Name;
        team.Category = request.Category;
        team.Coach = request.Coach ?? string.Empty;
        team.LogoUrl = request.LogoUrl ?? string.Empty;

        team.StatsTeam.Won = request.StatsTeam.Won;
        team.StatsTeam.Lost = request.StatsTeam.Lost;
        team.StatsTeam.Tied = request.StatsTeam.Tied;

        await _teamRepository.UpdateAsync(team, cancellationToken);
        
        return true;
    }
}