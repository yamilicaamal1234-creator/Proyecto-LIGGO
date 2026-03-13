using mediatR;
using Liggo.Domain.Interfaces;
using Liggo.Application.Interfaces;

namespace Liggo.Application.Functions.Players.Command
{
    public class DeletePlayerCommand : IRequest<bool>
    {
        public string PlayerId { get; set; } = string.Empty;
    }

    public class DeletePlayerCommandHandler : IRequestHandler<DeletePlayerCommand, bool>
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ICurrentUserService _currentUserService;

        public DeletePlayerCommandHandler(IPlayerRepository playerRepository, ICurrentUserService currentUserService)
        {
            _playerRepository = playerRepository;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
        {
            string secureSchoolId = _currentUserService.SchoolId;

            var existingPlayer = await _playerRepository.GetByIdAsync(secureSchoolId, request.PlayerId, cancellationToken);

            if (existingPlayer == null)
            {
                throw new Exception("Jugador no encontrado");
            }

            await _playerRepository.DeleteAsync(secureSchoolId, request.PlayerId, cancellationToken);

            return true;
        }
    }
}