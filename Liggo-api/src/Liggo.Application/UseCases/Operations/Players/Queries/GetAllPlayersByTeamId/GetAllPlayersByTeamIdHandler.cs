using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Application.UseCases.Operations.Players.Dtos;
using Liggo.Application.UseCases.Operations.Players.Queries.Responses;

namespace Liggo.Application.UseCases.Operations.Players.Queries.GetAllPlayersByTeamId;

public class GetAllPlayersByTeamIdHandler : IRequestHandler<GetAllPlayersByTeamIdQuery, IEnumerable<PlayerResponse>>
{
    private readonly IPlayerRepository _playerRepository;

    public GetAllPlayersByTeamIdHandler(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<IEnumerable<PlayerResponse>> Handle(GetAllPlayersByTeamIdQuery request, CancellationToken cancellationToken)
    {
        var players = await _playerRepository.GetAllByTeamIdAsync(request.TeamId, cancellationToken);
        if (players == null || !players.Any()) return System.Linq.Enumerable.Empty<PlayerResponse>();

        return players.Select(player => new PlayerResponse(
            player.Id,
            new PlayerInfoDto(player.Info.Name, player.Info.Dob, player.Info.Gender, player.Info.PhotoUrl),
            player.Status,
            player.Team,
            player.Parents ?? new System.Collections.Generic.List<string>(),
            new PlayerStatsDto(player.Stats.Matches, player.Stats.Goals, player.Stats.Minutes, player.Stats.YellowCards, player.Stats.RedCards)
        ));
    }
}