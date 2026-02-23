using Liggo.Domain.Entities.Operations;

namespace Liggo.Application.Interfaces.Operations;

public interface ISystemUserRepository
{
    Task<SystemUser?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<SystemUser?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<IEnumerable<SystemUser>> GetAllBySchoolIdAsync(string schoolId, CancellationToken cancellationToken = default);
    
    Task AddAsync(SystemUser systemUser, CancellationToken cancellationToken = default);
    Task UpdateAsync(SystemUser systemUser, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}