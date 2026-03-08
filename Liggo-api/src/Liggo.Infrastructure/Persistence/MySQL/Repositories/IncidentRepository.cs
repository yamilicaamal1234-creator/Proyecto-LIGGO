using Liggo.Application.Interfaces.Operations;
using Liggo.Domain.Entities.Operations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liggo.Infrastructure.Persistence.MySQL.Repositories
{
    public class IncidentRepository : IIncidentRepository
    {
        private readonly ApplicationDbContext _context;

        public IncidentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Incident incident)
        {
            incident.CreatedAt = DateTime.UtcNow;
            await _context.Incidents.AddAsync(incident);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id, Guid adminId)
        {
            var incident = await _context.Incidents
                .FirstOrDefaultAsync(i => i.Id == id && i.AdminId == adminId);

            if (incident != null)
            {
                _context.Incidents.Remove(incident);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Incident>> GetAllByAdminIdAsync(Guid adminId)
        {
            return await _context.Incidents
                .Where(i => i.AdminId == adminId)
                .Include(i => i.Player)
                .OrderByDescending(i => i.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Incident>> GetAllByPlayerIdAsync(Guid playerId, Guid adminId)
        {
            return await _context.Incidents
                .Where(i => i.PlayerId == playerId && i.AdminId == adminId)
                .Include(i => i.Player)
                .OrderByDescending(i => i.CreatedAt)
                .ToListAsync();
        }

        public async Task<Incident?> GetByIdAsync(Guid id, Guid adminId)
        {
            return await _context.Incidents
                .Include(i => i.Player)
                .FirstOrDefaultAsync(i => i.Id == id && i.AdminId == adminId);
        }

        public async Task UpdateAsync(Incident incident)
        {
            incident.UpdatedAt = DateTime.UtcNow;
            _context.Incidents.Update(incident);
            await _context.SaveChangesAsync();
        }
    }
}