using Liggo.Domain.Entities.Operations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Liggo.Application.Interfaces.Operations
{
    public interface IRegistrationRepository
    {
        Task<Registration?> GetByIdAsync(Guid id, Guid adminId);
        Task<IEnumerable<Registration>> GetAllByAdminIdAsync(Guid adminId);
        Task AddAsync(Registration registration);
        Task UpdateAsync(Registration registration);
        Task DeleteAsync(Guid id, Guid adminId);
    }
}
