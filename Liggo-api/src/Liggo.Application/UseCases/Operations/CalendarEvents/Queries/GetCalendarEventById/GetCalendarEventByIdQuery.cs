using System.Collections.Generic;
using MediatR;
using Liggo.Application.UseCases.Operations.CalendarEvents.Commands.CreateCalendarEvent; // Reusamos los DTOs

namespace Liggo.Application.UseCases.Operations.CalendarEvents.Queries.GetCalendarEventById;

// El DTO general de respuesta
public record CalendarEventResponse(
    string Id,
    string Type,
    EventMetadataDto Metadata,
    EventLocationDto Location,
    string Status,
    EventScoreDto Score,
    Dictionary<string, AttendanceRecordDto> Attendance);

public record GetCalendarEventByIdQuery(string Id) : IRequest<CalendarEventResponse?>;