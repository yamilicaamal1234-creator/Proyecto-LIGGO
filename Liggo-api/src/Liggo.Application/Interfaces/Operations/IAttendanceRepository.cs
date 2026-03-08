using Liggo.Domain.Entities.Operations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Liggo.Application.Interfaces.Operations
{
    public interface IAttendanceRepository
    {
        Task<IEnumerable<Attendance>> GetAttendancesByMatchIdAsync(Guid matchId, Guid adminId);
        Task AddOrUpdateAttendanceAsync(IEnumerable<Attendance> attendances);
    }
}
