using ClinicSoft.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicSoft.Data.Configuration;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("Patients");
        builder.HasKey(p => p.Id);

        builder.OwnsOne(p => p.Name, name =>
        {
            name.Property(n => n.FirstName).HasColumnName("FirstName").HasMaxLength(100).IsRequired();
            name.Property(n => n.LastName).HasColumnName("LastName").HasMaxLength(100).IsRequired(false);
        });

        builder.Property(p => p.Email).IsRequired().HasMaxLength(255);
        builder.Property(p => p.Phone).HasMaxLength(20).IsRequired(false);
        builder.Property(p => p.DateOfBirth).IsRequired();
        builder.Property(p => p.Gender).HasConversion<string>().HasMaxLength(20).IsRequired();
        builder.Property(p => p.BloodType).HasConversion<string>().HasMaxLength(10).IsRequired();
        builder.Property(p => p.Allergies).HasMaxLength(2000).IsRequired(false);
        builder.Property(p => p.ChronicConditions).HasMaxLength(2000).IsRequired(false);
        builder.Property(p => p.IsActive).HasDefaultValue(true).IsRequired();
        builder.Property(p => p.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.OwnsOne(p => p.Nif, nif =>
        {
            nif.Property(n => n.Number).HasColumnName("Nif").HasMaxLength(9).IsRequired(false);
        });

        builder.OwnsOne(p => p.CartaoCidadao, cc =>
        {
            cc.Property(c => c.Number).HasColumnName("CartaoCidadao").HasMaxLength(20).IsRequired(false);
        });

        builder.OwnsOne(p => p.NumeroUtenteSns, sns =>
        {
            sns.Property(n => n.Number).HasColumnName("NumeroUtenteSns").HasMaxLength(9).IsRequired(false);
        });

        builder.OwnsOne(p => p.Address, address =>
        {
            address.Property(a => a.Street).HasColumnName("Street").HasMaxLength(200).IsRequired(false);
            address.Property(a => a.Number).HasColumnName("AddressNumber").HasMaxLength(20).IsRequired(false);
            address.Property(a => a.Parish).HasColumnName("Parish").HasMaxLength(100).IsRequired(false);
            address.Property(a => a.Municipality).HasColumnName("Municipality").HasMaxLength(100).IsRequired(false);
            address.Property(a => a.District).HasColumnName("District").HasMaxLength(100).IsRequired(false);
            address.Property(a => a.PostalCode).HasColumnName("PostalCode").HasMaxLength(8).IsRequired(false);
            address.Property(a => a.Complement).HasColumnName("Complement").HasMaxLength(100).IsRequired(false);
            address.Ignore(a => a.FullAddress);
        });

        builder.Ignore(p => p.Age);

        builder.HasIndex(p => p.Email).IsUnique();
        builder.HasIndex(p => new { p.IsDeleted });

        builder.HasOne(p => p.InsurancePlan)
            .WithMany(ip => ip.Patients)
            .HasForeignKey(p => p.InsurancePlanId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(p => p.Appointments)
            .WithOne(a => a.Patient)
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.MedicalRecords)
            .WithOne(mr => mr.Patient)
            .HasForeignKey(mr => mr.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Exams)
            .WithOne(e => e.Patient)
            .HasForeignKey(e => e.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Bills)
            .WithOne(b => b.Patient)
            .HasForeignKey(b => b.PatientId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
