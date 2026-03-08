using Liggo.Domain.Entities.Operations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Liggo.Application.Interfaces.Operations
{
    public interface IMatchRepository
    {
        Task<Match?> GetByIdAsync(Guid id, Guid adminId);
        Task<IEnumerable<Match>> GetAllByAdminIdAsync(Guid adminId);
        Task AddAsync(Match match);
        Task UpdateAsync(Match match);
        Task DeleteAsync(Guid id, Guid adminId);
    }
}
