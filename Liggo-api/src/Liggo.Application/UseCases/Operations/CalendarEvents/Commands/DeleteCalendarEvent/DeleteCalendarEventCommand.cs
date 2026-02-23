using MediatR;

public record DeleteCalendarEventCommand(string Id) : IRequest<bool>;