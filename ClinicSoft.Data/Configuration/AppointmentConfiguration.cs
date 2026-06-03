using ClinicSoft.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicSoft.Data.Configuration;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable("Appointments");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.ScheduledAt).IsRequired();
        builder.Property(a => a.DurationMinutes).IsRequired().HasDefaultValue(30);
        builder.Property(a => a.Status).HasConversion<string>().HasMaxLength(30).IsRequired();
        builder.Property(a => a.Type).HasConversion<string>().HasMaxLength(30).IsRequired();
        builder.Property(a => a.Notes).HasMaxLength(2000).IsRequired(false);
        builder.Property(a => a.CancellationReason).HasMaxLength(500).IsRequired(false);
        builder.Property(a => a.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.OwnsOne(a => a.ConsultationFee, money =>
        {
            money.Property(m => m.Amount).HasColumnName("ConsultationFeeAmount")
                .HasColumnType("decimal(18,2)").IsRequired(false);
            money.Property(m => m.Currency).HasColumnName("ConsultationFeeCurrency")
                .HasMaxLength(3).HasDefaultValue("EUR").IsRequired(false);
        });

        builder.Ignore(a => a.DomainEvents);

        builder.HasIndex(a => new { a.DoctorId, a.ScheduledAt }).HasDatabaseName("IX_Appointments_Doctor_Date");
        builder.HasIndex(a => new { a.PatientId, a.Status });

        builder.HasOne(a => a.Patient)
            .WithMany(p => p.Appointments)
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Doctor)
            .WithMany(d => d.Appointments)
            .HasForeignKey(a => a.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Room)
            .WithMany(r => r.Appointments)
            .HasForeignKey(a => a.RoomId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(a => a.MedicalRecord)
            .WithOne(mr => mr.Appointment)
            .HasForeignKey<MedicalRecord>(mr => mr.AppointmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.Bill)
            .WithOne(b => b.Appointment)
            .HasForeignKey<Bill>(b => b.AppointmentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
