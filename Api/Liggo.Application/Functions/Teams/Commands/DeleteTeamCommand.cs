using MediatR;
using Liggo.Domain.Interfaces;
using Liggo.Application.Interfaces;
using FluentValidation;

namespace Liggo.Application.Functions.Teams.Commands
{
    public class DeleteTeamCommand : IRequest<string>
    {
        public string TeamId { get; set; } = string.Empty;
    }

    public class DeleteTeamCommandValidator : AbstractValidator<DeleteTeamCommand>
    {
        public DeleteTeamCommandValidator()
        {
            RuleFor(x => x.TeamId).NotEmpty().WithMessage("El equipo es requerido");
        }
    }

    public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand, string>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteTeamCommandHandler(ITeamRepository teamRepository, ICurrentUserService currentUserService)
        {
            _teamRepository = teamRepository;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
        {
            var schoolId = _currentUserService.SchoolId;
            var existingTeam = await _teamRepository.GetByIdAsync(schoolId, request.TeamId, cancellationToken);
            if (existingTeam == null)
            {
                throw new Exception("El equipo no existe.");
            }

            await _teamRepository.DeleteAsync(schoolId, request.TeamId, cancellationToken);
            return "El equipo fue eliminado correctamente.";
        }
    }
}