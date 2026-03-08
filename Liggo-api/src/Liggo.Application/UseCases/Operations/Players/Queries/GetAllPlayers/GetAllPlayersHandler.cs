using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Application.UseCases.Operations.Players.Dtos;

namespace Liggo.Application.UseCases.Operations.Players.Queries.GetAllPlayers
{
    public class GetAllPlayersHandler : IRequestHandler<GetAllPlayersQuery, IEnumerable<PlayerDto>>
    {
        private readonly IPlayerRepository _playerRepository;

        public GetAllPlayersHandler(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<IEnumerable<PlayerDto>> Handle(GetAllPlayersQuery request, CancellationToken cancellationToken)
        {
            var players = await _playerRepository.GetAllByAdminIdAsync(request.AdminId);

            return players.Select(player => new PlayerDto
            {
                Id = player.Id,
                FullName = player.FullName,
                DateOfBirth = player.DateOfBirth,
                AssignedTeam = player.AssignedTeam,
                GuardianName = player.GuardianName,
                GuardianPhone = player.GuardianPhone,
                Relationship = player.Relationship,
                CreatedAt = player.CreatedAt
            });
        }
    }
}
