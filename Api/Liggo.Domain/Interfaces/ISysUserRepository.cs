using Liggo.Domain.Entities.Documents;

namespace Liggo.Domain.Interfaces
{
    public interface ISysUserRepository
    {
        Task<SysUser?> GetByIdAsync(string firebaseUid, CancellationToken cancellationToken);
        Task AddAsync(SysUser sysUser, CancellationToken cancellationToken);
        Task UpdateAsync(SysUser sysUser, CancellationToken cancellationToken);
        Task AddTenantAccessAsync(string firebaseUid, string newSchoolId, CancellationToken cancellationToken);
    }
}