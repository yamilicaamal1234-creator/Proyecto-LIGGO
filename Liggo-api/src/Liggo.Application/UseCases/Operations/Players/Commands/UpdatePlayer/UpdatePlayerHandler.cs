using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;

namespace Liggo.Application.UseCases.Operations.Players.Commands.UpdatePlayer;

public class UpdatePlayerHandler : IRequestHandler<UpdatePlayerCommand, bool>
{
    private readonly IPlayerRepository _playerRepository;

    public UpdatePlayerHandler(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<bool> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = await _playerRepository.GetByIdAsync(request.Id, cancellationToken);
        if (player == null) return false;

        player.Status = request.Status;
        player.Team = request.Team;
        player.Parents = request.Parents ?? new System.Collections.Generic.List<string>();
        
        player.Info.Name = request.Info.Name;
        player.Info.Dob = request.Info.Dob;
        player.Info.Gender = request.Info.Gender;
        player.Info.PhotoUrl = request.Info.PhotoUrl;

        player.Stats.Matches = request.Stats.Matches;
        player.Stats.Goals = request.Stats.Goals;
        player.Stats.Minutes = request.Stats.Minutes;
        player.Stats.YellowCards = request.Stats.YellowCards;
        player.Stats.RedCards = request.Stats.RedCards;

        await _playerRepository.UpdateAsync(player, cancellationToken);
        
        return true;
    }
}