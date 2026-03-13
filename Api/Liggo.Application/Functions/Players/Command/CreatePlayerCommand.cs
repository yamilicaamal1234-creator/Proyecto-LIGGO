using FluentValidation;
using MediatR;
using Liggo.Domain.Documents;
using Liggo.Domain.Interfaces;
using Liggo.Application.Interfaces;
using System.Security.Cryptography.X509Certificates;

namespace Liggo.Application.Functions.Players.Command
{
    public class CreatePlayerCommand : IRequest <string>
    {
        public string Name { get; set; } = string.Empty;
        public string Dob { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string TeamId { get; set; } = string.Empty;
        public string Weight { get; set; } = string.Empty;
        public string Height { get; set; } = string.Empty;
    }

    public class CreatePlayerCommandValidator : AbstractValidator<CreatePlayerCommand>
    {
        public CreatePlayerCommandValidator()
        {
            RuleFor(x = x.name).NotEmpty().WithMessage("El nombre del jugador es requerido");
            RuleFor(x = x.Dob).NotEmpty().WithMessage("La fecha de nacimiento del jugador es requerida");
            RuleFor(x = x.Gender).NotEmpty().WithMessage("El género del jugador es requerido");
            RuleFor(x = x.Position).NotEmpty().WithMessage("La posición del jugador es requerida");
            RuleFor(x = x.TeamId).NotEmpty().WithMessage("El equipo es requerido");
        }
    }

    public class CratePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, string>
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ISchoolRepository _schoolRepository;
        
        public CreatePlayerCommandHandler(IPlayerReporitory playerRepository, ICurrentUserService _currentUserService, ISchoolRepository _schoolRepository)
        {
            _playerRepository = playerRepository;
            this._currentUserService = _currentUserService;
            this._schoolRepository = _schoolRepository;
        }

        public async Task<string> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {
            string secureSchoolId = _currentUserService.SchoolId;

            bool hasReachedLimit = await _schoolRepository.HasReachedPlayerLimitAsync(secureSchoolId, 30 ,cancellationToken);

            if (hasReachedLimit)
            {
                throw new Exception("Has alcanzado el límite de jugadores por equipo. Por favor, selecciona otro equipo.");
            }

            var newPlayer = new Player
            {
                Id = Guid.NewGuid().ToString(),
                TeamId = request.TeamId,
                Info = new PlayerInfo
                {
                    Name = request.Name,
                    Dob = request.Dob,
                    Gender = request.Gender,
                    Position = request.Position,
                    Weight = request.Weight,
                    Height = request.Height
                }
            };

            await _playerRepository.AddAsync(secureSchoolId, newPlayer, cancellationToken);

            return newPlayer.Id;
        }
    }
}