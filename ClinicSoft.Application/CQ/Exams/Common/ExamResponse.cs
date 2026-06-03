using ClinicSoft.Domain.Model.Enums;

namespace ClinicSoft.Application.CQ.Exams.Common;

public class ExamResponse
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public Guid? MedicalRecordId { get; set; }
    public ExamType Type { get; set; }
    public ExamStatus Status { get; set; }
    public DateTime RequestedAt { get; set; }
    public DateTime? ResultAt { get; set; }
    public string? ResultNotes { get; set; }
    public string? ResultFileUrl { get; set; }
    public string? RequestingDoctorNotes { get; set; }
    public DateTime? CreatedAt { get; set; }
}
