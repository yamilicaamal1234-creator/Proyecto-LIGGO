using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Liggo.Domain.Entities.Billing;

namespace Liggo.Infrastructure.Persistence.MySQL.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(c => c.Id);

        // CRÍTICO: El ExternalId (Firebase OrgId) debe ser único por si acaso, 
        // y lo indexamos para que las búsquedas desde Operations sean rapidísimas.
        builder.Property(c => c.ExternalId)
            .IsRequired()
            .HasMaxLength(100);
        builder.HasIndex(c => c.ExternalId).IsUnique();

        builder.Property(c => c.BusinessName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.TaxId)
            .IsRequired()
            .HasMaxLength(20); // Suficiente para RFCs y TaxIDs internacionales

        builder.Property(c => c.AdminEmail)
            .IsRequired()
            .HasMaxLength(150);

        // Relaciones: Un Customer tiene muchas Subscriptions
        builder.HasMany(c => c.Subscriptions)
            .WithOne(s => s.Customer)
            .HasForeignKey(s => s.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}