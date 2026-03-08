using Liggo.Application.Interfaces.Operations;
using Liggo.Domain.Entities.Operations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liggo.Infrastructure.Persistence.MySQL.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly ApplicationDbContext _context;

        public MatchRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Match match)
        {
            match.CreatedAt = DateTime.UtcNow;
            await _context.Matches.AddAsync(match);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id, Guid adminId)
        {
            var match = await _context.Matches
                .FirstOrDefaultAsync(m => m.Id == id && m.AdminId == adminId);

            if (match != null)
            {
                _context.Matches.Remove(match);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Match>> GetAllByAdminIdAsync(Guid adminId)
        {
            return await _context.Matches
                .Where(m => m.AdminId == adminId)
                .OrderByDescending(m => m.DateTime)
                .ToListAsync();
        }

        public async Task<Match?> GetByIdAsync(Guid id, Guid adminId)
        {
            return await _context.Matches
                .FirstOrDefaultAsync(m => m.Id == id && m.AdminId == adminId);
        }

        public async Task UpdateAsync(Match match)
        {
            match.UpdatedAt = DateTime.UtcNow;
            _context.Matches.Update(match);
            await _context.SaveChangesAsync();
        }
    }
}
