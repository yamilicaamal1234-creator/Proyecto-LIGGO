using MediatR;
using Liggo.Application.Interfaces.Operations;

namespace Liggo.Application.UseCases.Operations.CalendarEvents.Commands.UpdateCalendarEvent;

public class UpdateCalendarEventHandler : IRequestHandler<UpdateCalendarEventCommand, bool>
{
    private readonly ICalendarEventRepository _calendarEventRepository;

    public UpdateCalendarEventHandler(ICalendarEventRepository calendarEventRepository)
    {
        _calendarEventRepository = calendarEventRepository;
    }

    public async Task<bool> Handle(UpdateCalendarEventCommand request, CancellationToken cancellationToken)
    {
        var calendarEvent = await _calendarEventRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (calendarEvent == null) return false;

        // Actualización de propiedades
        calendarEvent.Type = request.Type;
        calendarEvent.Status = request.Status;
        
        calendarEvent.Metadata.Title = request.Metadata.Title;
        calendarEvent.Metadata.Category = request.Metadata.Category;
        calendarEvent.Metadata.Start = request.Metadata.Start;
        calendarEvent.Metadata.End = request.Metadata.End;

        calendarEvent.Location.Name = request.Location.Name;
        calendarEvent.Location.Geo = request.Location.Geo;

        calendarEvent.Score.Home = request.Score.Home;
        calendarEvent.Score.Away = request.Score.Away;

        calendarEvent.Attendance = request.Attendance.ToDictionary(
            k => k.Key,
            v => new Liggo.Domain.Entities.Operations.AttendanceRecord 
            {
                Status = v.Value.Status,
                Minutes = v.Value.Minutes,
                Goals = v.Value.Goals,
                Rating = v.Value.Rating,
                Note = v.Value.Note,
                Reason = v.Value.Reason
            });

        await _calendarEventRepository.UpdateAsync(calendarEvent, cancellationToken);
        
        return true;
    }
}