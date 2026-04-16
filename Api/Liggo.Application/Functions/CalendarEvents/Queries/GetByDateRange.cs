using MediatR;
using Liggo.Application.DTOs;
using Liggo.Domain.Interfaces;
using Liggo.Application.Interfaces;
using System.Linq;

namespace Liggo.Application.Functions.CalendarEvents.Queries
{
    public class GetEventDateRangeQuery : IRequest<IEnumerable<CalendarEventDto>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class GetEventDateRangeQueryHandler : IRequestHandler<GetEventDateRangeQuery, IEnumerable<CalendarEventDto>>
    {
        private readonly ICalendarRepository _calendarRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetEventDateRangeQueryHandler(ICalendarRepository calendarRepository, ICurrentUserService currentUserService)
        {
            _calendarRepository = calendarRepository;
            _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<CalendarEventDto>> Handle(GetEventDateRangeQuery request, CancellationToken cancellationToken)
        {
            var schoolId = _currentUserService.SchoolId;

            var calendarEvent = await _calendarRepository.GetByDateRangeAsync(schoolId, request.StartDate, request.EndDate, cancellationToken);

            if (calendarEvent == null)
            {
                throw new Exception("Evento no encontrado");
            }

            var dtos = calendarEvent.Select(calendarEvent => new CalendarEventDto
            {
                Id = calendarEvent.Id,
                EventType = calendarEvent.EventType,
                Status = calendarEvent.Status,
                Title = calendarEvent.MetaData.Title,
                DateStart = calendarEvent.MetaData.DateStart,
                DateEnd = calendarEvent.MetaData.DateEnd,
                LocationName = calendarEvent.MetaData.LocationName,
                Lat = calendarEvent.MetaData.Geo.Lat,
                Lng = calendarEvent.MetaData.Geo.Lng,
                
                // El diccionario se mapea igual, pero ahora se hace por cada evento de la lista
                Attendance = calendarEvent.AttendanceMap.ToDictionary(
                    kvp => kvp.Key, 
                    kvp => new AttendanceEntryDto 
                    {
                        Status = kvp.Value.Status,
                        Goals = kvp.Value.Goals,
                        Rating = kvp.Value.Rating,
                        Reason = kvp.Value.Reason
                    })
            }).ToList();


            return dtos;
        }
    }
}