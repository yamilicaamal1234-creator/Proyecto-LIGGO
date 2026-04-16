using FluentValidation;
using MediatR;
using Liggo.Domain.Entities.Documents;
using Liggo.Domain.Interfaces;
using Liggo.Application.Interfaces;
using System.Collections.Generic;

namespace Liggo.Application.Functions.CalendarEvents.Commands
{
    public class CreateEventCommand : IRequest<string>
    {
        public string EventType { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string LocationName { get; set; } = string.Empty;
        public double Lat { get; set; }
        public double Lng { get; set; } 
    }

    public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
    {
        public CreateEventCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("El título es obligatorio");
            RuleFor(x => x.DateStart).LessThan(x => x.DateEnd).WithMessage("La fecha de inicio debe ser anterior a la fecha de fin");
            RuleFor(x => x.LocationName).NotEmpty().WithMessage("El nombre del lugar es obligatorio");
        }
    }

    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, string>
    {
        private readonly ICalendarRepository _calendarRepository;
        private readonly ICurrentUserService _currentUserService;

        public CreateEventCommandHandler(ICalendarRepository calendarRepository, ICurrentUserService currentUserService)
        {
            _calendarRepository = calendarRepository;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var schoolId = _currentUserService.SchoolId;

            // Mapeo a la entidad de dominio de Firebase [cite: 47, 48, 49, 50]
            var newEvent = new CalendarEvent
            {
                Id = Guid.NewGuid().ToString(),
                EventType = request.EventType, // Mapeo directo al tipo definido 
                MetaData = new EventMetaData
                {
                    Title = request.Title,
                    DateStart = request.DateStart,
                    DateEnd = request.DateEnd,
                    LocationName = request.LocationName,
                    Geo = new GeoCoordinates // Estructura de coordenadas 
                    {
                        Lat = request.Lat,
                        Lng = request.Lng
                    }
                },
                Status = "scheduled",
                
                // Nace con el mapa de asistencia vacío [cite: 51, 52]
                AttendanceMap = new Dictionary<string, AttendanceEntry>() 
            };

            // Escritura directa en la sub-colección de la escuela en Firebase [cite: 68]
            await _calendarRepository.AddAsync(schoolId, newEvent, cancellationToken);

            return newEvent.Id;
        }
    }
}