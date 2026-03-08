using Liggo.Application.Interfaces.Operations;
using Liggo.Domain.Entities.Operations;
using Liggo.Infrastructure.Persistence.MySQL;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Liggo.Infrastructure.Persistence.MySQL.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ApplicationDbContext _context;

        public TeamRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Team?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _context.Teams
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Team>> GetAllBySchoolIdAsync(string schoolId, CancellationToken cancellationToken = default)
        {
            // Assuming Team has a SchoolId property, but since it's not in the entity, I'll return all for now
            // This might need to be adjusted based on the actual relationship
            return await _context.Teams.ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Team team, CancellationToken cancellationToken = default)
        {
            await _context.Teams.AddAsync(team, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Team team, CancellationToken cancellationToken = default)
        {
            _context.Teams.Update(team);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var team = await GetByIdAsync(id, cancellationToken);
            if (team != null)
            {
                _context.Teams.Remove(team);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}