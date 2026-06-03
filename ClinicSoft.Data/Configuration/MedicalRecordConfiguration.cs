using ClinicSoft.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicSoft.Data.Configuration;

public class MedicalRecordConfiguration : IEntityTypeConfiguration<MedicalRecord>
{
    public void Configure(EntityTypeBuilder<MedicalRecord> builder)
    {
        builder.ToTable("MedicalRecords");
        builder.HasKey(mr => mr.Id);

        builder.Property(mr => mr.Diagnosis).HasMaxLength(2000).IsRequired(false);
        builder.Property(mr => mr.ClinicalNotes).HasMaxLength(4000).IsRequired(false);
        builder.Property(mr => mr.TreatmentPlan).HasMaxLength(4000).IsRequired(false);
        builder.Property(mr => mr.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasOne(mr => mr.Patient)
            .WithMany(p => p.MedicalRecords)
            .HasForeignKey(mr => mr.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(mr => mr.Prescriptions)
            .WithOne(p => p.MedicalRecord)
            .HasForeignKey(p => p.MedicalRecordId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(mr => mr.Exams)
            .WithOne(e => e.MedicalRecord)
            .HasForeignKey(e => e.MedicalRecordId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
