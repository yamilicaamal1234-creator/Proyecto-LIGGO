namespace Liggo.Domain.Interfaces
{
    public interface IMemberReferenceRepository
    {
        // Para cuando el SysUser cambia su nombre globalmente
        Task UpdateExternalNameAsync(string firebaseUid, string newName, CancellationToken cancellationToken);
        
        // Para actualizaciones desde el módulo de Miembros (Nombre o Rol)
        Task UpdateReferenceAsync(string schoolId, string memberId, string? name, string? role, CancellationToken cancellationToken);
        
        // Útil para verificar si la referencia existe antes de un cobro
        Task<bool> ExistsAsync(string schoolId, string memberId, CancellationToken cancellationToken);
    }
}