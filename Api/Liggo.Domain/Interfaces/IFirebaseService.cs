namespace Liggo.Domain.Interfaces
{
    public interface IFirebaseService
    {
       // Se usa en el Onboarding: Crea el documento de la escuela en Firebase [cite: 104]
        Task CreateSchoolDocumentAsync(string schoolId, string name, int planId, CancellationToken cancellationToken);
        
        // Se usa al invitar a alguien: Crea su perfil en Firebase con una wallet inicial de $0 [cite: 120]
        Task CreateMemberProfileAsync(string schoolId, string memberId, string uid, string role, CancellationToken cancellationToken);
        
        // Se usa en el cobro recurrente: Actualiza masivamente los documentos de los miembros en Firebase para actualizar su wallet.balance [cite: 112]
        Task UpdateWalletBalanceAsync(string schoolId, string memberId, float newBalance, string newStatus, CancellationToken cancellationToken);
    }
}