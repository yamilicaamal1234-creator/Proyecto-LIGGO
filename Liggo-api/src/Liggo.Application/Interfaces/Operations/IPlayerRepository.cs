using Liggo.Domain.Entities.Operations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Liggo.Application.Interfaces.Operations
{
    public interface IPlayerRepository
    {
        Task<Player?> GetByIdAsync(Guid id, Guid adminId);
        Task<IEnumerable<Player>> GetAllByAdminIdAsync(Guid adminId);
        Task AddAsync(Player player);
        Task UpdateAsync(Player player);
        Task DeleteAsync(Guid id, Guid adminId);
    }
}