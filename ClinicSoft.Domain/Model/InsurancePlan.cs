using ClinicSoft.Domain.Common;
using ClinicSoft.Domain.Interfaces;
using ClinicSoft.Domain.Model.Enums;

namespace ClinicSoft.Domain.Model;

public class InsurancePlan : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? AnsCode { get; set; }
    public InsuranceType Type { get; set; }
    public decimal CoveragePercentage { get; set; }
    public string? ContactPhone { get; set; }
    public string? ContactEmail { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation
    public List<Patient> Patients { get; set; } = [];
}
