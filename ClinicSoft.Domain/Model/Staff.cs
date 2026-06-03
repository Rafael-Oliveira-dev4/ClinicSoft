using ClinicSoft.Domain.Common;
using ClinicSoft.Domain.Interfaces;
using ClinicSoft.Domain.Model.Enums;
using ClinicSoft.Domain.Model.ValueObjects;

namespace ClinicSoft.Domain.Model;

public class Staff : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public required Name Name { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public StaffRole Role { get; set; }
    public bool IsActive { get; set; } = true;
}
