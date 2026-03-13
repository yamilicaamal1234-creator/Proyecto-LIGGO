namespace Liggo.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        // Inicia el modo "espera", donde los cambios aún no son definitivos
        Task BeginTransactionAsync(CancellationToken cancellationToken);
        
        // Confirma y guarda los cambios permanentemente
        Task CommitAsync(CancellationToken cancellationToken);
        
        // Cancela todo y deja la base de datos como estaba antes del error
        Task RollbackAsync(CancellationToken cancellationToken);
        
        // Guarda cambios en memoria antes del Commit final
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}