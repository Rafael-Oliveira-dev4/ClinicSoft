using ClinicSoft.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicSoft.Data.Configuration;

public class ExamConfiguration : IEntityTypeConfiguration<Exam>
{
    public void Configure(EntityTypeBuilder<Exam> builder)
    {
        builder.ToTable("Exams");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Type).HasConversion<string>().HasMaxLength(50).IsRequired();
        builder.Property(e => e.Status).HasConversion<string>().HasMaxLength(30).IsRequired();
        builder.Property(e => e.RequestedAt).IsRequired();
        builder.Property(e => e.ResultAt).IsRequired(false);
        builder.Property(e => e.ResultNotes).HasMaxLength(4000).IsRequired(false);
        builder.Property(e => e.ResultFileUrl).HasMaxLength(500).IsRequired(false);
        builder.Property(e => e.RequestingDoctorNotes).HasMaxLength(1000).IsRequired(false);
        builder.Property(e => e.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.Ignore(e => e.DomainEvents);

        builder.HasIndex(e => new { e.PatientId, e.Status });

        builder.HasOne(e => e.Patient)
            .WithMany(p => p.Exams)
            .HasForeignKey(e => e.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.MedicalRecord)
            .WithMany(mr => mr.Exams)
            .HasForeignKey(e => e.MedicalRecordId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
