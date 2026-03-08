using Liggo.Application.Interfaces.Operations;
using Liggo.Domain.Entities.Operations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liggo.Infrastructure.Persistence.MySQL.Repositories
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly ApplicationDbContext _context;

        public RegistrationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Registration registration)
        {
            registration.CreatedAt = DateTime.UtcNow;
            await _context.Registrations.AddAsync(registration);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id, Guid adminId)
        {
            var registration = await _context.Registrations
                .FirstOrDefaultAsync(r => r.Id == id && r.AdminId == adminId);

            if (registration != null)
            {
                _context.Registrations.Remove(registration);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Registration>> GetAllByAdminIdAsync(Guid adminId)
        {
            return await _context.Registrations
                .Where(r => r.AdminId == adminId)
                .Include(r => r.Player) // Include player data
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task<Registration?> GetByIdAsync(Guid id, Guid adminId)
        {
            return await _context.Registrations
                .Include(r => r.Player) // Include player data
                .FirstOrDefaultAsync(r => r.Id == id && r.AdminId == adminId);
        }

        public async Task UpdateAsync(Registration registration)
        {
            registration.UpdatedAt = DateTime.UtcNow;
            _context.Registrations.Update(registration);
            await _context.SaveChangesAsync();
        }
    }
}
