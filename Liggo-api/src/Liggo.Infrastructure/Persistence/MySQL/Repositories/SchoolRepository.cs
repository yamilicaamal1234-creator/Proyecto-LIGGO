using Liggo.Application.Interfaces.Operations;
using Liggo.Domain.Entities.Operations;
using Liggo.Infrastructure.Persistence.MySQL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liggo.Infrastructure.Persistence.MySQL.Repositories
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly ApplicationDbContext _context;

        public SchoolRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<School?> GetByIdAsync(Guid id, Guid adminId)
        {
            return await _context.Schools
                .FirstOrDefaultAsync(s => s.Id == id && s.AdminId == adminId);
        }

        public async Task<IEnumerable<School>> GetAllByAdminIdAsync(Guid adminId)
        {
            return await _context.Schools
                .Where(s => s.AdminId == adminId)
                .ToListAsync();
        }

        public async Task AddAsync(School school)
        {
            school.CreatedAt = DateTime.UtcNow;
            await _context.Schools.AddAsync(school);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(School school)
        {
            var existingSchool = await _context.Schools
                .FirstOrDefaultAsync(s => s.Id == school.Id && s.AdminId == school.AdminId);

            if (existingSchool != null)
            {
                existingSchool.UpdatedAt = DateTime.UtcNow;
                existingSchool.Name = school.Name;
                existingSchool.Plan = school.Plan;
                existingSchool.LogoUrl = school.LogoUrl;
                existingSchool.Currency = school.Currency;
                existingSchool.Timezone = school.Timezone;
                // Categories would need special handling if not flattened or serialized

                _context.Schools.Update(existingSchool);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id, Guid adminId)
        {
            var school = await _context.Schools
                .FirstOrDefaultAsync(s => s.Id == id && s.AdminId == adminId);
            if (school != null)
            {
                _context.Schools.Remove(school);
                await _context.SaveChangesAsync();
            }
        }
    }
}