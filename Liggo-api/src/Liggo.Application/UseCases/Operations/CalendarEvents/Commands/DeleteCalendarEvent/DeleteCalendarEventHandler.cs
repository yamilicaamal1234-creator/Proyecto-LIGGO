using MediatR;
using Liggo.Application.Interfaces.Operations;

namespace Liggo.Application.UseCases.Operations.CalendarEvents.Commands.DeleteCalendarEvent;

public class DeleteCalendarEventHandler : IRequestHandler<DeleteCalendarEventCommand, bool>
{
    private readonly ICalendarEventRepository _calendarEventRepository;

    public DeleteCalendarEventHandler(ICalendarEventRepository calendarEventRepository)
    {
        _calendarEventRepository = calendarEventRepository;
    }

    public async Task<bool> Handle(DeleteCalendarEventCommand request, CancellationToken cancellationToken)
    {
        var calendarEvent = await _calendarEventRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (calendarEvent == null) return false;

        await _calendarEventRepository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}