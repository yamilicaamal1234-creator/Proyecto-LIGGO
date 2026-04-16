using MediatR;
using Liggo.Domain.Interfaces;
using Liggo.Application.Interfaces;

namespace Liggo.Application.Functions.CalendarEvents.Commands
{
    public class DeleteEventCommand : IRequest<bool>
    {
        public string Id { get; set; } = string.Empty;
    }

    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, bool>
    {
        private readonly ICalendarRepository _calendarRepository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteEventCommandHandler(ICalendarRepository calendarRepository, ICurrentUserService currentUserService)
        {
            _calendarRepository = calendarRepository;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(DeleteEventCommand request, CancellationToken)
        {
            var schoolId = _currentUserService.GetByIdAsync
        }
    }
}