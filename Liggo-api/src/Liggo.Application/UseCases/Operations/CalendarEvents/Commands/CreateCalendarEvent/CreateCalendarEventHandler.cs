using System;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Domain.Entities.Operations;

namespace Liggo.Application.UseCases.Operations.CalendarEvents.Commands.CreateCalendarEvent;

public class CreateCalendarEventHandler : IRequestHandler<CreateCalendarEventCommand, string>
{
    private readonly ICalendarEventRepository _calendarEventRepository;

    public CreateCalendarEventHandler(ICalendarEventRepository calendarEventRepository)
    {
        _calendarEventRepository = calendarEventRepository;
    }

    public async Task<string> Handle(CreateCalendarEventCommand request, CancellationToken cancellationToken)
    {
        var calendarEvent = new CalendarEvent
        {
            Id = Guid.NewGuid().ToString(), // Firebase puede auto-generarlo, pero es buena práctica crearlo aquí para devolverlo
            Type = request.Type,
            Status = request.Status,
            Metadata = new EventMetadata 
            {
                Title = request.Metadata.Title,
                Category = request.Metadata.Category,
                Start = request.Metadata.Start,
                End = request.Metadata.End
            },
            Location = new EventLocation 
            {
                Name = request.Location.Name,
                Geo = request.Location.Geo ?? new List<double>()
            },
            Score = new EventScore 
            {
                Home = request.Score.Home,
                Away = request.Score.Away
            },
            Attendance = request.Attendance.ToDictionary(
                k => k.Key,
                v => new AttendanceRecord 
                {
                    Status = v.Value.Status,
                    Minutes = v.Value.Minutes,
                    Goals = v.Value.Goals,
                    Rating = v.Value.Rating,
                    Note = v.Value.Note,
                    Reason = v.Value.Reason
                })
        };

        await _calendarEventRepository.AddAsync(calendarEvent, cancellationToken);

        return calendarEvent.Id;
    }
}