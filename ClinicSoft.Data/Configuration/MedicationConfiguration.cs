using ClinicSoft.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicSoft.Data.Configuration;

public class MedicationConfiguration : IEntityTypeConfiguration<Medication>
{
    public void Configure(EntityTypeBuilder<Medication> builder)
    {
        builder.ToTable("Medications");
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Name).IsRequired().HasMaxLength(200);
        builder.Property(m => m.ActiveSubstance).IsRequired().HasMaxLength(200);
        builder.Property(m => m.Category).HasConversion<string>().HasMaxLength(50).IsRequired();
        builder.Property(m => m.Unit).IsRequired().HasMaxLength(30);
        builder.Property(m => m.CurrentStock).IsRequired();
        builder.Property(m => m.MinimumStock).IsRequired();
        builder.Property(m => m.RequiresPrescription).HasDefaultValue(true).IsRequired();
        builder.Property(m => m.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.OwnsOne(m => m.UnitPrice, money =>
        {
            money.Property(p => p.Amount).HasColumnName("UnitPriceAmount")
                .HasColumnType("decimal(18,2)").IsRequired(false);
            money.Property(p => p.Currency).HasColumnName("UnitPriceCurrency")
                .HasMaxLength(3).HasDefaultValue("EUR").IsRequired(false);
        });

        builder.Ignore(m => m.IsLowStock);
        builder.Ignore(m => m.DomainEvents);

        builder.HasIndex(m => m.Name);
    }
}
