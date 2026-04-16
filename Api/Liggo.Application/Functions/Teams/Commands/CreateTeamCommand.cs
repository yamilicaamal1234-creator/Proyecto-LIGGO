using FluentValidation;
using MediatR;
using Liggo.Domain.Entities.Documents;
using Liggo.Domain.Interfaces;
using Liggo.Application.Interfaces;

namespace Liggo.Application.Functions.Teams.Commands
{
    public class CreateTeamCommand : IRequest<string>
    {
        public string SchoolId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string CoachId { get; set; } = string.Empty;
        public List<string> Roster { get; set; } = new();
    }

    public class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
    {
        public CreateTeamCommandValidator()
        {
            RuleFor(x => x.SchoolId).NotEmpty().WithMessage("La escuela es requerida");
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre del equipo es requerido");
            RuleFor(x => x.Category).NotEmpty().WithMessage("La categoría del equipo es requerida");
            RuleFor(x => x.CoachId).NotEmpty().WithMessage("El entrenador es requerido");
            RuleFor(x => x.Roster).NotEmpty().WithMessage("El roster del equipo es requerido");
        }
    }
    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, string>
    {
        private readonly ITeamRepository _teamRepository;

        public CreateTeamCommandHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<string> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            var newTeam = new Team
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Name,
                Category = request.Category,
                CoachId = request.CoachId,
                Roster = request.Roster
            };

            await _teamRepository.AddAsync(request.SchoolId, newTeam, cancellationToken);
            return newTeam.Id;
        }
    }   
}