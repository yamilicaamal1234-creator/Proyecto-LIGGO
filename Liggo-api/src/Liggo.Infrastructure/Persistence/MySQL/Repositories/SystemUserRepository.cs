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
    public class SystemUserRepository : ISystemUserRepository
    {
        private readonly ApplicationDbContext _context;

        public SystemUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SystemUser?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _context.SystemUsers
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public async Task<SystemUser?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _context.SystemUsers
                .FirstOrDefaultAsync(u => u.Auth.Email == email, cancellationToken);
        }

        public async Task<IEnumerable<SystemUser>> GetAllBySchoolIdAsync(string schoolId, CancellationToken cancellationToken = default)
        {
            // This might need adjustment based on how school relationship is modeled
            return await _context.SystemUsers.ToListAsync(cancellationToken);
        }

        public async Task AddAsync(SystemUser systemUser, CancellationToken cancellationToken = default)
        {
            systemUser.CreatedAt = DateTime.UtcNow;
            await _context.SystemUsers.AddAsync(systemUser, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(SystemUser systemUser, CancellationToken cancellationToken = default)
        {
            _context.SystemUsers.Update(systemUser);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var user = await GetByIdAsync(id, cancellationToken);
            if (user != null)
            {
                _context.SystemUsers.Remove(user);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}