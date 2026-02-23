using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Application.UseCases.Operations.Players.Dtos;
using Liggo.Application.UseCases.Operations.Players.Queries.Responses;

namespace Liggo.Application.UseCases.Operations.Players.Queries.GetPlayerById;

public class GetPlayerByIdHandler : IRequestHandler<GetPlayerByIdQuery, PlayerResponse?>
{
    private readonly IPlayerRepository _playerRepository;

    public GetPlayerByIdHandler(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<PlayerResponse?> Handle(GetPlayerByIdQuery request, CancellationToken cancellationToken)
    {
        var player = await _playerRepository.GetByIdAsync(request.Id, cancellationToken);
        if (player == null) return null;

        return new PlayerResponse(
            player.Id,
            new PlayerInfoDto(player.Info.Name, player.Info.Dob, player.Info.Gender, player.Info.PhotoUrl),
            player.Status,
            player.Team,
            player.Parents ?? new System.Collections.Generic.List<string>(),
            new PlayerStatsDto(player.Stats.Matches, player.Stats.Goals, player.Stats.Minutes, player.Stats.YellowCards, player.Stats.RedCards)
        );
    }
}