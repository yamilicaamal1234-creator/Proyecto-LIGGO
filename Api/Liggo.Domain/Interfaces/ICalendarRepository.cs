using Liggo.Domain.Entities.Documents;

namespace Liggo.Domain.Interfaces
{
    public interface ICalendarRepository
    {
        Task<CalendarEvent?> GetByIdAsync(string schoolId, string calendarId, CancellationToken cancellationToken);
        
        // Mejor buscar por rango de fechas que traer TODO el historial
        Task<IEnumerable<CalendarEvent>> GetByDateRangeAsync(string schoolId, DateTime start, DateTime end, CancellationToken cancellationToken);
        
        Task AddAsync(string schoolId, CalendarEvent calendarEvent, CancellationToken cancellationToken);
        Task UpdateAsync(string schoolId, CalendarEvent calendarEvent, CancellationToken cancellationToken);
        Task DeleteAsync(string schoolId, string calendarId, CancellationToken cancellationToken);
        Task UpdateAttendanceAsync(string schoolId, string eventId, string playerId, AttendanceEntry attendance, CancellationToken cancellationToken);
    }
}