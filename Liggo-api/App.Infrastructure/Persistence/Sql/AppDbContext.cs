using App.Domain.Entities.Sql;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Persistence.Sql;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Plan> Plans { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Tenant>().HasKey(t => t.IdTenant);
        modelBuilder.Entity<Plan>().HasKey(p => p.IdPlan);
        modelBuilder.Entity<Customer>().HasKey(c => c.IdCustomer);
        modelBuilder.Entity<Subscription>().HasKey(s => s.IdSub);
        
        modelBuilder.Entity<Customer>()
            .HasIndex(c => c.ExternalId)
            .IsUnique(); 
    }
}