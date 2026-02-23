using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Application.UseCases.Operations.Players.Dtos;
using Liggo.Application.UseCases.Operations.Players.Queries.Responses;

namespace Liggo.Application.UseCases.Operations.Players.Queries.GetAllPlayersBySchoolId;

public class GetAllPlayersBySchoolIdHandler : IRequestHandler<GetAllPlayersBySchoolIdQuery, IEnumerable<PlayerResponse>>
{
    private readonly IPlayerRepository _playerRepository;

    public GetAllPlayersBySchoolIdHandler(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<IEnumerable<PlayerResponse>> Handle(GetAllPlayersBySchoolIdQuery request, CancellationToken cancellationToken)
    {
        var players = await _playerRepository.GetAllBySchoolIdAsync(request.SchoolId, cancellationToken);
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