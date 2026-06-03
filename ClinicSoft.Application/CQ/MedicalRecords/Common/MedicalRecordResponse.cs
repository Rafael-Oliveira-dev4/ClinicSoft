namespace ClinicSoft.Application.CQ.MedicalRecords.Common;

public class MedicalRecordResponse
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public Guid AppointmentId { get; set; }
    public string? Diagnosis { get; set; }
    public string? ClinicalNotes { get; set; }
    public string? TreatmentPlan { get; set; }
    public List<PrescriptionResponse> Prescriptions { get; set; } = [];
    public DateTime? CreatedAt { get; set; }
}

public class PrescriptionResponse
{
    public Guid Id { get; set; }
    public required string MedicationName { get; set; }
    public required string Dosage { get; set; }
    public required string Frequency { get; set; }
    public required string Duration { get; set; }
    public string? Instructions { get; set; }
    public DateTime ValidUntil { get; set; }
}
