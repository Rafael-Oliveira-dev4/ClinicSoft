using ClinicSoft.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicSoft.Data.Configuration;

public class ProcedureConfiguration : IEntityTypeConfiguration<Procedure>
{
    public void Configure(EntityTypeBuilder<Procedure> builder)
    {
        builder.ToTable("Procedures");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
        builder.Property(p => p.Code).HasMaxLength(30).IsRequired(false);
        builder.Property(p => p.Category).HasConversion<string>().HasMaxLength(50).IsRequired();
        builder.Property(p => p.EstimatedDurationMinutes).IsRequired();
        builder.Property(p => p.IsActive).HasDefaultValue(true).IsRequired();
        builder.Property(p => p.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.OwnsOne(p => p.BasePrice, money =>
        {
            money.Property(m => m.Amount).HasColumnName("BasePriceAmount")
                .HasColumnType("decimal(18,2)").IsRequired(false);
            money.Property(m => m.Currency).HasColumnName("BasePriceCurrency")
                .HasMaxLength(3).HasDefaultValue("EUR").IsRequired(false);
        });

        builder.HasIndex(p => p.Code)
            .IsUnique()
            .HasFilter("[Code] IS NOT NULL");
    }
}
