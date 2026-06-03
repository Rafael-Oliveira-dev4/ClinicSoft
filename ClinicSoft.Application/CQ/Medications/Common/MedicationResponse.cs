using ClinicSoft.Domain.Model.Enums;

namespace ClinicSoft.Application.CQ.Medications.Common;

public class MedicationResponse
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string ActiveSubstance { get; set; }
    public MedicationCategory Category { get; set; }
    public required string Unit { get; set; }
    public int CurrentStock { get; set; }
    public int MinimumStock { get; set; }
    public bool IsLowStock { get; set; }
    public decimal? UnitPrice { get; set; }
    public bool RequiresPrescription { get; set; }
    public DateTime? CreatedAt { get; set; }
}
