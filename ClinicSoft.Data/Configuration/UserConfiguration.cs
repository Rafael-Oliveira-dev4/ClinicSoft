using ClinicSoft.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicSoft.Data.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Username).IsRequired().HasMaxLength(50)
            .HasConversion(v => v.ToLower(), v => v);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(255)
            .HasConversion(v => v.ToLower(), v => v);
        builder.Property(u => u.PasswordHash).IsRequired().HasMaxLength(500);
        builder.Property(u => u.Role).IsRequired().HasMaxLength(30).HasDefaultValue("Staff");
        builder.Property(u => u.ProfileImageUrl).HasMaxLength(500).IsRequired(false);
        builder.Property(u => u.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasIndex(u => u.Username).IsUnique();
        builder.HasIndex(u => u.Email).IsUnique();

        builder.HasOne(u => u.Doctor)
            .WithMany()
            .HasForeignKey(u => u.DoctorId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(u => u.Staff)
            .WithMany()
            .HasForeignKey(u => u.StaffId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
