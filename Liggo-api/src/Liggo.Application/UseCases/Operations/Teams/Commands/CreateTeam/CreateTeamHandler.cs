using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Domain.Entities.Operations;

namespace Liggo.Application.UseCases.Operations.Teams.Commands.CreateTeam;

public class CreateTeamHandler : IRequestHandler<CreateTeamCommand, string>
{
    private readonly ITeamRepository _teamRepository;

    public CreateTeamHandler(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<string> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var team = new Team
        {
            Id = Guid.NewGuid().ToString(),
            Name = request.Name,
            Category = request.Category,
            Coach = request.Coach ?? string.Empty,
            LogoUrl = request.LogoUrl ?? string.Empty,
            StatsTeam = new TeamStats
            {
                Won = request.StatsTeam.Won,
                Lost = request.StatsTeam.Lost,
                Tied = request.StatsTeam.Tied
            }
        };

        await _teamRepository.AddAsync(team, cancellationToken);

        return team.Id;
    }
}