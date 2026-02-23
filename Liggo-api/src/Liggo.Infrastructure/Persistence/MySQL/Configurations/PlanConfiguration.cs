using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Liggo.Domain.Entities.Billing;

namespace Liggo.Infrastructure.Persistence.MySQL.Configurations;

public class PlanConfiguration : IEntityTypeConfiguration<Plan>
{
    public void Configure(EntityTypeBuilder<Plan> builder)
    {
        builder.ToTable("Plans");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        // Importante en bases de datos: definir la precisión del dinero (18 dígitos, 2 decimales)
        builder.Property(p => p.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.Currency)
            .IsRequired()
            .HasMaxLength(3)
            .HasDefaultValue("MXN");

        builder.Property(p => p.Frequency)
            .IsRequired()
            .HasConversion<string>() // Guarda el Enum como texto ("Monthly", "Yearly") en vez de números para que sea legible en la BD
            .HasMaxLength(20);

        builder.Property(p => p.IsActive)
            .IsRequired()
            .HasDefaultValue(true);
    }
}