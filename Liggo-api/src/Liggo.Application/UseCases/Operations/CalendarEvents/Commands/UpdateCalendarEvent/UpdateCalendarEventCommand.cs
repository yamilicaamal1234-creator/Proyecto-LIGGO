using System.Collections.Generic;
using MediatR;
using Liggo.Application.UseCases.Operations.CalendarEvents.Commands.CreateCalendarEvent; // Reusamos los DTOs

namespace Liggo.Application.UseCases.Operations.CalendarEvents.Commands.UpdateCalendarEvent;

public record UpdateCalendarEventCommand(
    string Id,
    string Type,
    EventMetadataDto Metadata,
    EventLocationDto Location,
    string Status,
    EventScoreDto Score,
    Dictionary<string, AttendanceRecordDto> Attendance) : IRequest<bool>;