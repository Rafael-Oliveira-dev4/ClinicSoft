using ClinicSoft.Domain.Common;
using ClinicSoft.Domain.Interfaces;

namespace ClinicSoft.Domain.Model;

public class Prescription : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid MedicalRecordId { get; set; }
    public required string MedicationName { get; set; }
    public required string Dosage { get; set; }
    public required string Frequency { get; set; }
    public required string Duration { get; set; }
    public string? Instructions { get; set; }
    public DateTime ValidUntil { get; set; }

    // Navigation
    public MedicalRecord? MedicalRecord { get; set; }
}
