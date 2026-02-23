using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Liggo.Domain.Entities.Billing;

namespace Liggo.Infrastructure.Persistence.MySQL.Configurations;

public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.ToTable("Subscriptions");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Status)
            .IsRequired()
            .HasConversion<string>() // Guarda el Enum como texto ("Active", "Canceled")
            .HasMaxLength(30);

        builder.Property(s => s.StartDate)
            .IsRequired();

        builder.Property(s => s.EndDate)
            .IsRequired();

        builder.Property(s => s.AutoRenew)
            .IsRequired()
            .HasDefaultValue(true);

        // La relación con el Customer ya se definió en CustomerConfiguration, 
        // pero definiremos la relación con el Plan explícitamente aquí:
        builder.HasOne(s => s.Plan)
            .WithMany() // Un plan puede estar en muchas suscripciones, pero la entidad Plan no necesita una lista de Subscriptions
            .HasForeignKey(s => s.PlanId)
            .OnDelete(DeleteBehavior.Restrict); // No puedes borrar un plan si hay suscripciones activas usándolo
    }
}