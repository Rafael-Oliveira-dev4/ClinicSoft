using ClinicSoft.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicSoft.Data.Configuration;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable("Doctors");
        builder.HasKey(d => d.Id);

        builder.OwnsOne(d => d.Name, name =>
        {
            name.Property(n => n.FirstName).HasColumnName("FirstName").HasMaxLength(100).IsRequired();
            name.Property(n => n.LastName).HasColumnName("LastName").HasMaxLength(100).IsRequired(false);
        });

        builder.OwnsOne(d => d.CedulaProfissional, cp =>
        {
            cp.Property(c => c.Number).HasColumnName("CedulaProfissional").HasMaxLength(20).IsRequired();
        });

        builder.Property(d => d.Email).IsRequired().HasMaxLength(255);
        builder.Property(d => d.Phone).HasMaxLength(20).IsRequired(false);
        builder.Property(d => d.Specialty).HasConversion<string>().HasMaxLength(50).IsRequired();
        builder.Property(d => d.Bio).HasMaxLength(2000).IsRequired(false);
        builder.Property(d => d.ProfileImageUrl).HasMaxLength(500).IsRequired(false);
        builder.Property(d => d.IsActive).HasDefaultValue(true).IsRequired();
        builder.Property(d => d.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasIndex(d => d.Email).IsUnique();

        builder.HasMany(d => d.Appointments)
            .WithOne(a => a.Doctor)
            .HasForeignKey(a => a.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
