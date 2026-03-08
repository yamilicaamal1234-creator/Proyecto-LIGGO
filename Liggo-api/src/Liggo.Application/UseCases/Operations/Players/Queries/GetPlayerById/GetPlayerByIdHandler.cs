using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Application.UseCases.Operations.Players.Dtos;
using Liggo.Application.Exceptions;

namespace Liggo.Application.UseCases.Operations.Players.Queries.GetPlayerById
{
    public class GetPlayerByIdHandler : IRequestHandler<GetPlayerByIdQuery, PlayerDto>
    {
        private readonly IPlayerRepository _playerRepository;

        public GetPlayerByIdHandler(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<PlayerDto> Handle(GetPlayerByIdQuery request, CancellationToken cancellationToken)
        {
            var player = await _playerRepository.GetByIdAsync(request.Id, request.AdminId);

            if (player == null)
            {
                throw new NotFoundException($"Player with ID {request.Id} not found.");
            }

            return new PlayerDto
            {
                Id = player.Id,
                FullName = player.FullName,
                DateOfBirth = player.DateOfBirth,
                AssignedTeam = player.AssignedTeam,
                GuardianName = player.GuardianName,
                GuardianPhone = player.GuardianPhone,
                Relationship = player.Relationship,
                CreatedAt = player.CreatedAt
            };
        }
    }
}