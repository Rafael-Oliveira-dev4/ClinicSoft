using ClinicSoft.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicSoft.Data.Configuration;

public class StaffConfiguration : IEntityTypeConfiguration<Staff>
{
    public void Configure(EntityTypeBuilder<Staff> builder)
    {
        builder.ToTable("Staff");
        builder.HasKey(s => s.Id);

        builder.OwnsOne(s => s.Name, name =>
        {
            name.Property(n => n.FirstName).HasColumnName("FirstName").HasMaxLength(100).IsRequired();
            name.Property(n => n.LastName).HasColumnName("LastName").HasMaxLength(100).IsRequired(false);
        });

        builder.Property(s => s.Email).IsRequired().HasMaxLength(255);
        builder.Property(s => s.Phone).HasMaxLength(20).IsRequired(false);
        builder.Property(s => s.Role).HasConversion<string>().HasMaxLength(30).IsRequired();
        builder.Property(s => s.IsActive).HasDefaultValue(true).IsRequired();
        builder.Property(s => s.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasIndex(s => s.Email).IsUnique();
    }
}
