using Liggo.Domain.Entities.Operations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Liggo.Application.Interfaces.Operations
{
    public interface IPaymentRepository
    {
        Task<Payment?> GetByIdAsync(Guid id, Guid adminId);
        Task<IEnumerable<Payment>> GetAllByAdminIdAsync(Guid adminId);
        Task AddAsync(Payment payment);
        Task UpdateAsync(Payment payment);
        Task DeleteAsync(Guid id, Guid adminId);
    }
}
