using FluentValidation;
using MediatR;
using Liggo.Domain.Interfaces;
using Liggo.Application.Interfaces;

namespace Liggo.Application.Functions.Teams.Commands
{
    public class UpdateTeamCommand : IRequest<bool>
    {
        public string TeamId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string CoachId { get; set; } = string.Empty;
        public List<string> Roster { get; set; } = new();
    }

    public class UpdateTeamCommandValidator : AbstractValidator<UpdateTeamCommand>
    {
        public UpdateTeamCommandValidator()
        {
            RuleFor(x => x.TeamId).NotEmpty().WithMessage("El equipo es requerido");
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre del equipo es requerido");
            RuleFor(x => x.Category).NotEmpty().WithMessage("La categoría del equipo es requerida");
            RuleFor(x => x.CoachId).NotEmpty().WithMessage("El entrenador es requerido");
            RuleFor(x => x.Roster).NotEmpty().WithMessage("El roster del equipo es requerido");
        }
    }

    public class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand, bool>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ITeamRepository _teamRepository;

        public UpdateTeamCommandHandler(ITeamRepository teamRepository, ICurrentUserService currentUserService)
        {
            _teamRepository = teamRepository;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
        {
            var schoolId = _currentUserService.SchoolId;

            var existingTeam = await _teamRepository.GetByIdAsync(schoolId, request.TeamId, cancellationToken);
            if (existingTeam == null)
            {
                throw new Exception("El equipo no existe.");
            }

            existingTeam.Name = request.Name;
            existingTeam.Category = request.Category;
            existingTeam.CoachId = request.CoachId;
            existingTeam.Roster = request.Roster;

            await _teamRepository.UpdateAsync(schoolId, existingTeam, cancellationToken);
            return true;
        }
    }
}