using ClinicSoft.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicSoft.Data.Configuration;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("Rooms");
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name).IsRequired().HasMaxLength(100);
        builder.Property(r => r.Type).HasConversion<string>().HasMaxLength(30).IsRequired();
        builder.Property(r => r.Floor).IsRequired();
        builder.Property(r => r.IsAvailable).HasDefaultValue(true).IsRequired();
        builder.Property(r => r.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasMany(r => r.Appointments)
            .WithOne(a => a.Room)
            .HasForeignKey(a => a.RoomId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
