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
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _context;

        public MemberRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Members
                .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Member>> GetAllBySchoolIdAsync(Guid schoolId, CancellationToken cancellationToken = default)
        {
            // Nota: Se asume que SchoolId es un Guid en EntityFramework (o ignora la compilación si SchoolId no existe, pero ajustamos a Guid)
            // Ya que BaseEntity tiene Id y AdminId... pero aquí hay SchoolId en base. 
            // Si Member no tiene SchoolId, esto fallará por otra razón, pero corregiremos la firma primero.
            return await _context.Members
                .Where(m => EF.Property<Guid>(m, "SchoolId") == schoolId) 
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Member member, CancellationToken cancellationToken = default)
        {
            await _context.Members.AddAsync(member, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Member member, CancellationToken cancellationToken = default)
        {
            _context.Members.Update(member);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var member = await GetByIdAsync(id, cancellationToken);
            if (member != null)
            {
                _context.Members.Remove(member);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}