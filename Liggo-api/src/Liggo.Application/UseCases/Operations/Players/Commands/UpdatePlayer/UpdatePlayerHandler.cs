using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Exceptions;
using Liggo.Application.Interfaces.Operations;

namespace Liggo.Application.UseCases.Operations.Players.Commands.UpdatePlayer
{
    public class UpdatePlayerHandler : IRequestHandler<UpdatePlayerCommand, Unit>
    {
        private readonly IPlayerRepository _playerRepository;

        public UpdatePlayerHandler(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<Unit> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
        {
            var player = await _playerRepository.GetByIdAsync(request.Id, request.AdminId);
            if (player == null)
            {
                throw new NotFoundException($"Player with ID {request.Id} not found.");
            }

            // Actualizar las propiedades de la entidad
            player.FullName = request.FullName;
            player.DateOfBirth = request.DateOfBirth;
            player.AssignedTeam = request.AssignedTeam;
            player.GuardianName = request.GuardianName;
            player.GuardianPhone = request.GuardianPhone;
            player.Relationship = request.Relationship;

            await _playerRepository.UpdateAsync(player);
            
            return Unit.Value;
        }
    }
}