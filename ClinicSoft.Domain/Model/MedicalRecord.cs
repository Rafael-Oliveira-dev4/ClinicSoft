using ClinicSoft.Domain.Common;
using ClinicSoft.Domain.Interfaces;

namespace ClinicSoft.Domain.Model;

public class MedicalRecord : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public Guid AppointmentId { get; set; }
    public string? Diagnosis { get; set; }
    public string? ClinicalNotes { get; set; }
    public string? TreatmentPlan { get; set; }

    // Navigation
    public Patient? Patient { get; set; }
    public Appointment? Appointment { get; set; }
    public List<Prescription> Prescriptions { get; set; } = [];
    public List<Exam> Exams { get; set; } = [];
}
