using FluentValidation;
using MediatR;
using Liggo.Domain.Interfaces;
using Liggo.Application.Interfaces;
using System.Collections.Generic;

namespace Liggo.Application.Functions.CalendarEvents.Commands
{
    // ==========================================================
    // 1. EL COMANDO (Lo que manda el Coach desde la App al tomar lista)
    // ==========================================================
    public class UpdateAttendanceCommand : IRequest<bool>
    {
        public string EventId { get; set; } = string.Empty;
        public string PlayerId { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // "present" o "absent"
        
        // Datos extra opcionales [cite: 54, 55]
        public int? Goals { get; set; }
        public int? Rating { get; set; }
        public string? Reason { get; set; }
    }

    // ==========================================================
    // 2. EL VALIDADOR (Lógica de negocio estricta)
    // ==========================================================
    public class UpdateAttendanceCommandValidator : AbstractValidator<UpdateAttendanceCommand>
    {
        public UpdateAttendanceCommandValidator()
        {
            RuleFor(x => x.EventId).NotEmpty().WithMessage("El evento es obligatorio.");
            RuleFor(x => x.PlayerId).NotEmpty().WithMessage("El jugador es obligatorio.");
            
            RuleFor(x => x.Status).Must(x => new[] { "present", "absent" }.Contains(x))
                .WithMessage("El estado solo puede ser 'present' o 'absent'.");

            // Validaciones condicionales lógicas
            When(x => x.Status == "absent", () => {
                RuleFor(x => x.Goals).Null().WithMessage("Un jugador ausente no puede tener goles.");
                RuleFor(x => x.Rating).Null().WithMessage("Un jugador ausente no puede tener calificación.");
            });
        }
    }

    // ==========================================================
    // 3. EL HANDLER (El Parche Dinámico)
    // ==========================================================
    public class UpdateAttendanceCommandHandler : IRequestHandler<UpdateAttendanceCommand, bool>
    {
        private readonly ICalendarRepository _calendarRepository;
        private readonly ICurrentUserService _currentUserService;

        public UpdateAttendanceCommandHandler(ICalendarRepository calendarRepository, ICurrentUserService currentUserService)
        {
            _calendarRepository = calendarRepository;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(UpdateAttendanceCommand request, CancellationToken cancellationToken)
        {
            var schoolId = _currentUserService.SchoolId;

            // 1. ARMAMOS EL OBJETO DE ASISTENCIA PARA ESTE JUGADOR EN ESPECÍFICO
            var playerData = new Dictionary<string, object>
            {
                { "status", request.Status }
            };

            if (request.Status == "present")
            {
                if (request.Goals.HasValue) playerData.Add("goals", request.Goals.Value);
                if (request.Rating.HasValue) playerData.Add("rating", request.Rating.Value);
            }
            else if (request.Status == "absent")
            {
                if (!string.IsNullOrWhiteSpace(request.Reason)) playerData.Add("reason", request.Reason);
            }

            // 2. ¡EL TRUCO MAESTRO DE FIREBASE! 🪄
            // En lugar de sobrescribir todo el 'attendance_map', usamos interpolación 
            // para decirle a Firebase: "Entra a attendance_map, y SOLO actualiza la llave de ESTE jugador"
            string firebaseDynamicKey = $"attendance_map.{request.PlayerId}";

            var updates = new Dictionary<string, object>
            {
                { firebaseDynamicKey, playerData }
            };

            // 3. ENVIAMOS EL PARCHE
            await _calendarRepository.UpdatePartialAsync(schoolId, request.EventId, updates, cancellationToken);

            return true;
        }
    }
}