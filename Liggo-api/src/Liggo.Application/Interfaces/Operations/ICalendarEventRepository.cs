using Liggo.Domain.Entities.Operations;

namespace Liggo.Application.Interfaces.Operations;

public interface ICalendarEventRepository
{
    Task<CalendarEvent?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<IEnumerable<CalendarEvent>> GetAllBySchoolIdAsync(string schoolId, CancellationToken cancellationToken = default);
    Task<IEnumerable<CalendarEvent>> GetAllByTeamIdAsync(string teamId, CancellationToken cancellationToken = default);
    
    Task AddAsync(CalendarEvent calendarEvent, CancellationToken cancellationToken = default);
    Task UpdateAsync(CalendarEvent calendarEvent, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}