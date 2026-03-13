using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Liggo.Domain.Entities.Relational;

namespace Liggo.Domain.Interfaces
{
    public interface IApplicationDbContext
    {
        // Las 4 tablas principales que definiste en tu documento
        DbSet<School> Schools { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<MemberReference> MembersReference { get; set; }
        DbSet<LedgerTransaction> LedgerTransactions { get; set; }

        // Expone la infraestructura de la base de datos para poder crear Transacciones
        DatabaseFacade Database { get; }

        // El método para confirmar los cambios en MySQL
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}