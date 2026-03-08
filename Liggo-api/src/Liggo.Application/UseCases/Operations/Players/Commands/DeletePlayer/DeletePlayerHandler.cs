using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Application.Exceptions;

namespace Liggo.Application.UseCases.Operations.Players.Commands.DeletePlayer
{
    public class DeletePlayerHandler : IRequestHandler<DeletePlayerCommand, Unit>
    {
        private readonly IPlayerRepository _playerRepository;

        public DeletePlayerHandler(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<Unit> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
        {
            var player = await _playerRepository.GetByIdAsync(request.Id, request.AdminId);
            if (player == null)
            {
                throw new NotFoundException($"Player with ID {request.Id} not found.");
            }

            await _playerRepository.DeleteAsync(request.Id, request.AdminId);
            
            return Unit.Value;
        }
    }
}