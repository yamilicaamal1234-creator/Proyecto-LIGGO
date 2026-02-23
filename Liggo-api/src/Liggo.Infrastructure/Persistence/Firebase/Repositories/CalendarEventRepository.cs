using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Liggo.Application.Interfaces.Operations;
using Liggo.Domain.Entities.Operations;

namespace Liggo.Infrastructure.Persistence.Firebase.Repositories;

public class CalendarEventRepository : ICalendarEventRepository
{
    public Task<CalendarEvent?> GetByIdAsync(string id, CancellationToken cancellationToken = default) => throw new System.NotImplementedException();
    public Task<IEnumerable<CalendarEvent>> GetAllBySchoolIdAsync(string schoolId, CancellationToken cancellationToken = default) => throw new System.NotImplementedException();
    public Task<IEnumerable<CalendarEvent>> GetAllByTeamIdAsync(string teamId, CancellationToken cancellationToken = default) => throw new System.NotImplementedException();
    public Task AddAsync(CalendarEvent calendarEvent, CancellationToken cancellationToken = default) => throw new System.NotImplementedException();
    public Task UpdateAsync(CalendarEvent calendarEvent, CancellationToken cancellationToken = default) => throw new System.NotImplementedException();
    public Task DeleteAsync(string id, CancellationToken cancellationToken = default) => throw new System.NotImplementedException();
}