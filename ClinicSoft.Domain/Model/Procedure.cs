using ClinicSoft.Domain.Common;
using ClinicSoft.Domain.Interfaces;
using ClinicSoft.Domain.Model.Enums;
using ClinicSoft.Domain.Model.ValueObjects;

namespace ClinicSoft.Domain.Model;

public class Procedure : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Code { get; set; }
    public MedicalSpecialty Category { get; set; }
    public Money? BasePrice { get; set; }
    public int EstimatedDurationMinutes { get; set; }
    public bool IsActive { get; set; } = true;
}
