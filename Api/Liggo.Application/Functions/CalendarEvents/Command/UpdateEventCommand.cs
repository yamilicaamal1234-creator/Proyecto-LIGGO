using FluentValidation;
using MediatR;
using Liggo.Domain.Interfaces;
using Liggo.Application.Interfaces;
using System.Collections.Generic;

namespace Liggo.Application.Functions.CalendarEvents.Commands
{
    // ==========================================================
    // 1. EL COMANDO (Todo es opcional excepto el ID)
    // ==========================================================
    public class UpdateEventCommand : IRequest<bool>
    {
        public string EventId { get; set; } = string.Empty;
        
        // Propiedades opcionales (Si Angular no las manda, llegan como null)
        public string? Title { get; set; }
        public string? Type { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public string? LocationName { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public string? Status { get; set; }
    }

    // ==========================================================
    // 2. EL VALIDADOR (Validación condicional)
    // ==========================================================
    public class UpdateEventCommandValidator : AbstractValidator<UpdateEventCommand>
    {
        public UpdateEventCommandValidator()
        {
            RuleFor(x => x.EventId).NotEmpty().WithMessage("El ID del evento es obligatorio");

            // Validamos 'Type' SOLO si Angular decidió mandarlo [cite: 48]
            When(x => x.Type != null, () => {
                RuleFor(x => x.Type).Must(x => new[] { "match", "practice", "event" }.Contains(x))
                    .WithMessage("El tipo de evento debe ser: match, practice o event");
            });

            // Validamos 'Status' SOLO si lo están intentando cambiar [cite: 50]
            When(x => x.Status != null, () => {
                RuleFor(x => x.Status).Must(x => new[] { "scheduled", "live", "finalized" }.Contains(x))
                    .WithMessage("El estado debe ser: scheduled, live o finalized");
            });

            // Si mandan ambas fechas, validamos que tengan sentido lógico
            When(x => x.DateStart.HasValue && x.DateEnd.HasValue, () => {
                RuleFor(x => x.DateStart).LessThan(x => x.DateEnd)
                    .WithMessage("La fecha de inicio debe ser anterior a la de fin");
            });
        }
    }

    // ==========================================================
    // 3. EL HANDLER (El constructor del Parche)
    // ==========================================================
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, bool>
    {
        private readonly ICalendarRepository _calendarRepository;
        private readonly ICurrentUserService _currentUserService;

        public UpdateEventCommandHandler(ICalendarRepository calendarRepository, ICurrentUserService currentUserService)
        {
            _calendarRepository = calendarRepository;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var schoolId = _currentUserService.SchoolId;

            // 1. CREAMOS EL DICCIONARIO (El parche para Firebase)
            var updates = new Dictionary<string, object>();

            // 2. LLENAMOS EL PARCHE USANDO DOT NOTATION PARA OBJETOS ANIDADOS [cite: 49]
            if (request.Title != null) updates.Add("metadata.title", request.Title);
            if (request.Type != null) updates.Add("type", request.Type);
            
            // Usamos .Value porque DateTime es un nullable struct (DateTime?)
            if (request.DateStart.HasValue) updates.Add("metadata.date_start", request.DateStart.Value);
            if (request.DateEnd.HasValue) updates.Add("metadata.date_end", request.DateEnd.Value);
            
            if (request.LocationName != null) updates.Add("metadata.location_name", request.LocationName);
            
            if (request.Lat.HasValue) updates.Add("metadata.geo.lat", request.Lat.Value);
            if (request.Lng.HasValue) updates.Add("metadata.geo.lng", request.Lng.Value);
            
            if (request.Status != null) updates.Add("status", request.Status);

            // 3. ENVIAMOS EL PARCHE A FIREBASE
            if (updates.Count > 0)
            {
                // Un viaje ultrarrápido y seguro a la base de datos
                await _calendarRepository.UpdatePartialAsync(schoolId, request.EventId, updates, cancellationToken);
            }

            return true;
        }
    }
}