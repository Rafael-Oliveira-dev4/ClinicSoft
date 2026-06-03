using ClinicSoft.Domain.Model.Enums;

namespace ClinicSoft.Domain.Events;

public class ExamResultAvailableEvent : DomainEvent
{
    public Guid ExamId { get; }
    public Guid PatientId { get; }
    public ExamType ExamType { get; }

    public ExamResultAvailableEvent(Guid examId, Guid patientId, ExamType examType)
    {
        ExamId = examId;
        PatientId = patientId;
        ExamType = examType;
    }
}
