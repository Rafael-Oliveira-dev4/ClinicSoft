using ClinicSoft.Domain.Common;
using ClinicSoft.Domain.Events;
using ClinicSoft.Domain.Interfaces;
using ClinicSoft.Domain.Model.Enums;

namespace ClinicSoft.Domain.Model;

public class Exam : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public Guid? MedicalRecordId { get; set; }
    public ExamType Type { get; set; }
    public ExamStatus Status { get; set; } = ExamStatus.Requested;
    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ResultAt { get; set; }
    public string? ResultNotes { get; set; }
    public string? ResultFileUrl { get; set; }
    public string? RequestingDoctorNotes { get; set; }

    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void MarkResultAvailable(string resultNotes, string? fileUrl = null)
    {
        Status = ExamStatus.ResultAvailable;
        ResultNotes = resultNotes;
        ResultFileUrl = fileUrl;
        ResultAt = DateTime.UtcNow;
        _domainEvents.Add(new ExamResultAvailableEvent(Id, PatientId, Type));
        UpdateTimestamp();
    }

    public void ClearDomainEvents() => _domainEvents.Clear();

    // Navigation
    public Patient? Patient { get; set; }
    public MedicalRecord? MedicalRecord { get; set; }
}
