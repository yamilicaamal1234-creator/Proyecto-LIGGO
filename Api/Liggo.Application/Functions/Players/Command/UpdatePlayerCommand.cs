using FluentValidation;
using MediatR;
using Liggo.Domain.Interfaces;
using Liggo.Application.Interfaces;

namespace Liggo.Application.Functions.Players.Command
{
    public class UpdatePlayerCommand : IRequest<bool>
    {
        public string PlayerId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Dob { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Weight { get; set; } = string.Empty;
        public string Height { get; set; } = string.Empty;
    }

    public class UpdatePlayerCommandHandler : IRequestHandler<UpdatePlayerCommand, bool>
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ICurrentUserService _currentUserService;

        public UpdatePlayerCommandHandler(IPlayerRepository playerRepository, ICurrentUserService currentUserService)
        {
            _playerRepository = playerRepository;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
        {
            string secureSchoolId = _currentUserService.SchoolId;

            var existingPlayer = await _playerRepository.GetByIdAsync(secureSchoolId, request.PlayerId, cancellationToken);

            if (existingPlayer == null)
            {
                throw new Exception("Jugador no encontrado");
            }

            existingPlayer.Info.Name = request.Name;
            existingPlayer.Info.Dob = request.Dob;
            existingPlayer.Info.Gender = request.Gender;
            existingPlayer.Info.Position = request.Position;
            existingPlayer.Info.Weight = request.Weight;
            existingPlayer.Info.Height = request.Height;

            await _playerRepository.UpdateAsync(secureSchoolId, existingPlayer, cancellationToken);

            return true;
        }
    }
}