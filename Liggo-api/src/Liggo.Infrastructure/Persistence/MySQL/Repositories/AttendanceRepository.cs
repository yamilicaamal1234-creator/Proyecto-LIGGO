using Liggo.Application.Interfaces.Operations;
using Liggo.Domain.Entities.Operations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liggo.Infrastructure.Persistence.MySQL.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Attendance>> GetAttendancesByMatchIdAsync(Guid matchId, Guid adminId)
        {
            return await _context.Attendances
                .Where(a => a.MatchId == matchId && a.AdminId == adminId)
                .Include(a => a.Player)
                .ToListAsync();
        }

        public async Task AddOrUpdateAttendanceAsync(IEnumerable<Attendance> attendances)
        {
            foreach (var attendanceRecord in attendances)
            {
                var existingRecord = await _context.Attendances
                    .FirstOrDefaultAsync(a => a.PlayerId == attendanceRecord.PlayerId && a.MatchId == attendanceRecord.MatchId);

                if (existingRecord != null)
                {
                    // Update existing record
                    existingRecord.Status = attendanceRecord.Status;
                    existingRecord.UpdatedAt = DateTime.UtcNow;
                }
                else
                {
                    // Add new record
                    attendanceRecord.CreatedAt = DateTime.UtcNow;
                    _context.Attendances.Add(attendanceRecord);
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
