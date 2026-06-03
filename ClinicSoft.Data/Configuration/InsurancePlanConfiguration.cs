using ClinicSoft.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicSoft.Data.Configuration;

public class InsurancePlanConfiguration : IEntityTypeConfiguration<InsurancePlan>
{
    public void Configure(EntityTypeBuilder<InsurancePlan> builder)
    {
        builder.ToTable("InsurancePlans");
        builder.HasKey(ip => ip.Id);

        builder.Property(ip => ip.Name).IsRequired().HasMaxLength(200);
        builder.Property(ip => ip.AnsCode).HasMaxLength(20).IsRequired(false);
        builder.Property(ip => ip.Type).HasConversion<string>().HasMaxLength(30).IsRequired();
        builder.Property(ip => ip.CoveragePercentage).HasColumnType("decimal(5,2)").IsRequired();
        builder.Property(ip => ip.ContactPhone).HasMaxLength(20).IsRequired(false);
        builder.Property(ip => ip.ContactEmail).HasMaxLength(255).IsRequired(false);
        builder.Property(ip => ip.IsActive).HasDefaultValue(true).IsRequired();
        builder.Property(ip => ip.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasMany(ip => ip.Patients)
            .WithOne(p => p.InsurancePlan)
            .HasForeignKey(p => p.InsurancePlanId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
