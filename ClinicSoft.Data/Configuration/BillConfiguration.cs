using ClinicSoft.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicSoft.Data.Configuration;

public class BillConfiguration : IEntityTypeConfiguration<Bill>
{
    public void Configure(EntityTypeBuilder<Bill> builder)
    {
        builder.ToTable("Bills");
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Status).HasConversion<string>().HasMaxLength(20).IsRequired();
        builder.Property(b => b.DueDate).IsRequired();
        builder.Property(b => b.PaidAt).IsRequired(false);
        builder.Property(b => b.Notes).HasMaxLength(1000).IsRequired(false);
        builder.Property(b => b.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.OwnsOne(b => b.GrossAmount, money =>
        {
            money.Property(m => m.Amount).HasColumnName("GrossAmount")
                .HasColumnType("decimal(18,2)").IsRequired();
            money.Property(m => m.Currency).HasColumnName("GrossCurrency")
                .HasMaxLength(3).HasDefaultValue("EUR").IsRequired();
        });

        builder.OwnsOne(b => b.InsuranceCoverage, money =>
        {
            money.Property(m => m.Amount).HasColumnName("InsuranceCoverageAmount")
                .HasColumnType("decimal(18,2)").IsRequired();
            money.Property(m => m.Currency).HasColumnName("InsuranceCoverageCurrency")
                .HasMaxLength(3).HasDefaultValue("EUR").IsRequired();
        });

        builder.OwnsOne(b => b.PatientAmount, money =>
        {
            money.Property(m => m.Amount).HasColumnName("PatientAmount")
                .HasColumnType("decimal(18,2)").IsRequired();
            money.Property(m => m.Currency).HasColumnName("PatientCurrency")
                .HasMaxLength(3).HasDefaultValue("EUR").IsRequired();
        });

        builder.HasIndex(b => new { b.PatientId, b.Status });

        builder.HasOne(b => b.Patient)
            .WithMany(p => p.Bills)
            .HasForeignKey(b => b.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Appointment)
            .WithOne(a => a.Bill)
            .HasForeignKey<Bill>(b => b.AppointmentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
