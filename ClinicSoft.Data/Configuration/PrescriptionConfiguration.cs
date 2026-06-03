using ClinicSoft.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicSoft.Data.Configuration;

public class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
{
    public void Configure(EntityTypeBuilder<Prescription> builder)
    {
        builder.ToTable("Prescriptions");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.MedicationName).IsRequired().HasMaxLength(200);
        builder.Property(p => p.Dosage).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Frequency).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Duration).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Instructions).HasMaxLength(1000).IsRequired(false);
        builder.Property(p => p.ValidUntil).IsRequired();
        builder.Property(p => p.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasOne(p => p.MedicalRecord)
            .WithMany(mr => mr.Prescriptions)
            .HasForeignKey(p => p.MedicalRecordId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
