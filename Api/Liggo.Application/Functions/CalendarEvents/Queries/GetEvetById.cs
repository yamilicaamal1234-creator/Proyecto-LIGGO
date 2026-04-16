using MediatR;
using Liggo.Application.DTOs;
using Liggo.Domain.Interfaces;
using Liggo.Application.Interfaces;

namespace Liggo.Application.Functions.CalendarEvents.Queries
{
    public class GetEventByIdQuery : IRequest<CalendarEventDto>
    {
        public string EventId { get; set; } = string.Empty;
    }

    public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, CalendarEventDto>
    {
        private readonly ICalendarRepository _calendarRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetEventByIdQueryHandler(ICalendarRepository calendarRepository, ICurrentUserService currentUserService)
        {
            _calendarRepository = calendarRepository;
            _currentUserService = currentUserService;
        }

        public async Task<CalendarEventDto> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var schoolId = _currentUserService.SchoolId;

            var calendarEvent = await _calendarRepository.GetByIdAsync(schoolId, request.EventId, cancellationToken);

            if (calendarEvent == null)
            {
                throw new Exception("Evento no encontrado");
            }

            var dto = new CalendarEventDto
            {
                Id = calendarEvent.Id,
                EventType = calendarEvent.EventType,
                Status = calendarEvent.Status,
                Title = calendarEvent.MetaData.Title,
                DateStart = calendarEvent.MetaData.DateStart,
                DateEnd = calendarEvent.MetaData.DateEnd,
                LocationName = calendarEvent.MetaData.LocationName,
                AttendanceStatus = calendarEvent.AttendanceMap.ContainsKey(schoolId) ? calendarEvent.AttendanceMap[schoolId].Status : "unknown",
                Goals = calendarEvent.AttendanceMap.ContainsKey(schoolId) ? calendarEvent.AttendanceMap[schoolId].Goals : null,
                Rating = calendarEvent.AttendanceMap.ContainsKey(schoolId) ? calendarEvent.AttendanceMap[schoolId].Rating : null,
                Reason = calendarEvent.AttendanceMap.ContainsKey(schoolId) ? calendarEvent.AttendanceMap[schoolId].Reason : null,
                Lat = calendarEvent.MetaData.Geo.Lat,
                Lng = calendarEvent.MetaData.Geo.Lng,
                Attendance = calendarEvent.AttendanceMap.ToDictionary(
                    kvp => kvp.Key,
                    kvp => new AttendanceEntryDto 
                    {
                        Status = kvp.Value.Status,
                        Goals = kvp.Value.Goals,
                        Rating = kvp.Value.Rating,
                        Reason = kvp.Value.Reason
                    })
            };

            return dto;
        }
    }
}