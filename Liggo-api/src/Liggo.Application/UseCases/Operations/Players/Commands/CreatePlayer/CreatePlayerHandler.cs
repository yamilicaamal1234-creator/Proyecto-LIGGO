using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Domain.Entities.Operations;

namespace Liggo.Application.UseCases.Operations.Players.Commands.CreatePlayer;

public class CreatePlayerHandler : IRequestHandler<CreatePlayerCommand, string>
{
    private readonly IPlayerRepository _playerRepository;

    public CreatePlayerHandler(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<string> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = new Player
        {
            Id = Guid.NewGuid().ToString(),
            Status = request.Status,
            Team = request.Team,
            Parents = request.Parents ?? new System.Collections.Generic.List<string>(),
            Info = new PlayerInfo
            {
                Name = request.Info.Name,
                Dob = request.Info.Dob,
                Gender = request.Info.Gender,
                PhotoUrl = request.Info.PhotoUrl
            },
            Stats = new PlayerStats
            {
                Matches = request.Stats.Matches,
                Goals = request.Stats.Goals,
                Minutes = request.Stats.Minutes,
                YellowCards = request.Stats.YellowCards,
                RedCards = request.Stats.RedCards
            }
        };

        await _playerRepository.AddAsync(player, cancellationToken);

        return player.Id;
    }
}