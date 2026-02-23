using System;
using System.Collections.Generic;
using MediatR;

namespace Liggo.Application.UseCases.Operations.CalendarEvents.Commands.CreateCalendarEvent;

// DTOs anidados para mantener limpio el Command
public record EventMetadataDto(string Title, string Category, DateTime Start, DateTime End);
public record EventLocationDto(string Name, List<double> Geo);
public record EventScoreDto(int Home, int Away);
public record AttendanceRecordDto(string Status, int Minutes, int Goals, int Rating, string Note, string Reason);

public record CreateCalendarEventCommand(
    string Type,
    EventMetadataDto Metadata,
    EventLocationDto Location,
    string Status,
    EventScoreDto Score,
    Dictionary<string, AttendanceRecordDto> Attendance) : IRequest<string>; // Retorna el string ID de Firebase