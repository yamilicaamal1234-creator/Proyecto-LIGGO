using MediatR;
using Liggo.Application.DTOs;
using Liggo.Domain.Interfaces;
using Liggo.Application.Interfaces;

namespace Liggo.Application.Functions.Players.Queries
{
    // 1. El Query ahora está vacío porque Angular no necesita decirnos de qué escuela es
    public class GetPlayersBySchoolQuery : IRequest<List<PlayerDto>>
    {
    }

    // 2. Corregido a QueryHandler
    public class GetPlayersBySchoolQueryHandler : IRequestHandler<GetPlayersBySchoolQuery, List<PlayerDto>>
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetPlayersBySchoolQueryHandler(IPlayerRepository playerRepository, ICurrentUserService currentUserService)
        {
            _playerRepository = playerRepository;
            _currentUserService = currentUserService;
        }

        public async Task<List<PlayerDto>> Handle(GetPlayersBySchoolQuery request, CancellationToken cancellationToken)
        {
            // La seguridad manda: sacamos el tenant del token
            string secureSchoolId = _currentUserService.SchoolId;

            var players = await _playerRepository.GetAllBySchoolAsync(secureSchoolId, cancellationToken);

            var dtos = players.Select(player => new PlayerDto
            {
                Id = player.Id,
                FullName = player.Info.Name,
                Position = player.Info.Position,
                AgeOrDob = player.Info.Dob,
                TotalGoals = player.Stats.Goals,
                AverageRating = player.Stats.AvgRating // Corregido a Average
            }).ToList();

            return dtos;
        }
    }
}