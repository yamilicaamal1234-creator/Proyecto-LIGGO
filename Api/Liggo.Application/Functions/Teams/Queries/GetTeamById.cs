using MediatR;
using Liggo.Application.DTOs;
using Liggo.Domain.Interfaces;
using Liggo.Application.Interfaces;

namespace Liggo.Application.Functions.Teams.Queries
{
    public class GetTeamByIdQuery : IRequest<TeamDto>
    {
        public string TeamId { get; set; } = string.Empty;
    }

    public class GetTeamByIdHandler : IRequestHandler<GetTeamByIdQuery, TeamDto>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ISysUserRepository _sysUserRepository;

        public GetTeamByIdHandler(ITeamRepository teamRepository, ICurrentUserService currentUserService, ISysUserRepository sysUserRepository)
        {
            _teamRepository = teamRepository;
            _currentUserService = currentUserService;
            _sysUserRepository = sysUserRepository;
        }

        public async Task<TeamDto> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
        {
            string secureSchoolId = _currentUserService.SchoolId;

            var team = await _teamRepository.GetByIdAsync(secureSchoolId, request.TeamId, cancellationToken);
            if (team == null) throw new Exception("Team no encontrado");

            var coachInfo = await _sysUserRepository.GetByIdAsync(team.CoachId, cancellationToken);

            var dto = new TeamDto
            {
                CoachId = team.CoachId,
                Name = team.Name,
                Category = team.Category,
                CoachName = coachInfo != null ? coachInfo.FullName : "Desconocido",
                Roster = team.Roster
            };
            return dto;
        }
    }
}