using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Liggo.Domain.Entities.Billing;
using Liggo.Domain.Entities.Operations;

namespace Liggo.Infrastructure.Persistence.MySQL;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
    }

    // Las tablas de nuestra base de datos
    public DbSet<Tenant> Tenants { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Plan> Plans { get; set; } = null!;
    public DbSet<Subscription> Subscriptions { get; set; } = null!;
    public DbSet<Player> Players { get; set; } = null!;
    public DbSet<Registration> Registrations { get; set; } = null!;
    public DbSet<Match> Matches { get; set; } = null!;
    public DbSet<Attendance> Attendances { get; set; } = null!;
    public DbSet<Payment> Payments { get; set; } = null!;
    public DbSet<Incident> Incidents { get; set; } = null!;

    // DbSets que faltaban y causaban errores de compilación
    public DbSet<LedgerTransaction> LedgerTransactions { get; set; } = null!;
    public DbSet<Member> Members { get; set; } = null!;
    public DbSet<School> Schools { get; set; } = null!;
    public DbSet<SystemUser> SystemUsers { get; set; } = null!;
    public DbSet<Team> Teams { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Esta línea es magia pura: le dice a EF Core que busque en esta misma 
        // carpeta todos los archivos que hereden de IEntityTypeConfiguration 
        // y los aplique automáticamente. ¡Así no tenemos que registrarlos uno por uno!
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}