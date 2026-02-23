using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Application.UseCases.Operations.CalendarEvents.Commands.CreateCalendarEvent;

namespace Liggo.Application.UseCases.Operations.CalendarEvents.Queries.GetCalendarEventById;

public class GetCalendarEventByIdHandler : IRequestHandler<GetCalendarEventByIdQuery, CalendarEventResponse?>
{
    private readonly ICalendarEventRepository _calendarEventRepository;

    public GetCalendarEventByIdHandler(ICalendarEventRepository calendarEventRepository)
    {
        _calendarEventRepository = calendarEventRepository;
    }

    public async Task<CalendarEventResponse?> Handle(GetCalendarEventByIdQuery request, CancellationToken cancellationToken)
    {
        var evt = await _calendarEventRepository.GetByIdAsync(request.Id, cancellationToken);

        if (evt == null) return null;

        return new CalendarEventResponse(
            evt.Id,
            evt.Type,
            new EventMetadataDto(evt.Metadata.Title, evt.Metadata.Category, evt.Metadata.Start, evt.Metadata.End),
            new EventLocationDto(evt.Location.Name, evt.Location.Geo),
            evt.Status,
            new EventScoreDto(evt.Score.Home, evt.Score.Away),
            evt.Attendance.ToDictionary(
                k => k.Key,
                v => new AttendanceRecordDto(v.Value.Status, v.Value.Minutes, v.Value.Goals, v.Value.Rating, v.Value.Note, v.Value.Reason)
            )
        );
    }
}