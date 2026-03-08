using Liggo.Domain.Entities.Operations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Liggo.Application.Interfaces.Operations
{
    public interface ISchoolRepository
    {
        Task<School?> GetByIdAsync(Guid id, Guid adminId);
        Task<IEnumerable<School>> GetAllByAdminIdAsync(Guid adminId);
        Task AddAsync(School school);
        Task UpdateAsync(School school);
        Task DeleteAsync(Guid id, Guid adminId);
    }
}
