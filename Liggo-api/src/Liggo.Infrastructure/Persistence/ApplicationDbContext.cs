using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Liggo.Domain.Entities.Billing;

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Esta línea es magia pura: le dice a EF Core que busque en esta misma 
        // carpeta todos los archivos que hereden de IEntityTypeConfiguration 
        // y los aplique automáticamente. ¡Así no tenemos que registrarlos uno por uno!
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}