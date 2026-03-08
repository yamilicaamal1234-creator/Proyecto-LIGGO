using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Domain.Entities.Operations;

namespace Liggo.Application.UseCases.Operations.Players.Commands.CreatePlayer
{
    public class CreatePlayerHandler : IRequestHandler<CreatePlayerCommand, Guid>
    {
        private readonly IPlayerRepository _playerRepository;

        public CreatePlayerHandler(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<Guid> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {
            var player = new Player
            {
                Id = Guid.NewGuid(),
                AdminId = request.AdminId,
                FullName = request.FullName,
                DateOfBirth = request.DateOfBirth,
                AssignedTeam = request.AssignedTeam,
                GuardianName = request.GuardianName,
                GuardianPhone = request.GuardianPhone,
                Relationship = request.Relationship
            };

            await _playerRepository.AddAsync(player);

            return player.Id;
        }
    }
}