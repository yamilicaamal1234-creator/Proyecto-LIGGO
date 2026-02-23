using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Liggo.Domain.Entities.Billing;

namespace Liggo.Infrastructure.Persistence.MySQL.Configurations;

public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.ToTable("Tenants");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(t => t.ApiKey)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(t => t.WebhookUrl)
            .HasMaxLength(500);

        // Relaciones: Un Tenant tiene muchos Customers
        builder.HasMany(t => t.Customers)
            .WithOne(c => c.Tenant)
            .HasForeignKey(c => c.TenantId)
            .OnDelete(DeleteBehavior.Restrict); // Evita borrar un Tenant si tiene Customers activos

        // Relaciones: Un Tenant tiene muchos Plans
        builder.HasMany(t => t.Plans)
            .WithOne(p => p.Tenant)
            .HasForeignKey(p => p.TenantId)
            .OnDelete(DeleteBehavior.Cascade); // Si se borra el Tenant, se borran sus planes
    }
}