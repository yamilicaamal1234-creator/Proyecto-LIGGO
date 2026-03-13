using MediatR;
using Liggo.Application.DTOs;
using Liggo.Domain.Interfaces;
using Liggo.Application.Interfaces;

namespace Liggo.Application.Functions.Players.Queries
{
    public class GetPlayerByIdQuery : IRequest<PlayerDto>
    {
        public string PlayerId { get; set; } = string.Empty;
    }

    public class GetPlayerByIdQueryHandler : IRequestHandler<GetPlayerByIdQuery, PlayerDto>
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetPlayerByIdQueryHandler(IPlayerRepository playerRepository, ICurrentUserService currentUserService)
        {
            _playerRepository = playerRepository;
            _currentUserService = currentUserService;
        }

        public async Task<PlayerDto> Handle(GetPlayerByIdQuery request, CancellationToken cancellationToken)
        {
            string secureSchoolId = _currentUserService.SchoolId;

            var player = await _playerRepository.GetByIdAsync(secureSchoolId, request.PlayerId, cancellationToken);

            if (player == null)
            {
                throw new Exception("Jugador no encontrado");
            }

            var dto = new PlayerDto
            {
                Id = player.Id,
                FullName = player.Info.Name,
                Position = player.Info.Position,
                AgeOrDob = player.Info.Dob,
                TotalGoals = player.Stats.Goals,
                AverangeRating = player.Stats.AvgRating
            };

            return dto;
        }
    }
}