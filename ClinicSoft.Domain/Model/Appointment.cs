using ClinicSoft.Domain.Common;
using ClinicSoft.Domain.Events;
using ClinicSoft.Domain.Interfaces;
using ClinicSoft.Domain.Model.Enums;
using ClinicSoft.Domain.Model.ValueObjects;

namespace ClinicSoft.Domain.Model;

public class Appointment : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }
    public Guid? RoomId { get; set; }
    public DateTime ScheduledAt { get; set; }
    public int DurationMinutes { get; set; } = 30;
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;
    public AppointmentType Type { get; set; }
    public string? Notes { get; set; }
    public string? CancellationReason { get; set; }
    public Money? ConsultationFee { get; set; }

    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    // Navigation
    public Patient? Patient { get; set; }
    public Doctor? Doctor { get; set; }
    public Room? Room { get; set; }
    public MedicalRecord? MedicalRecord { get; set; }
    public Bill? Bill { get; set; }

    public void UpdateStatus(AppointmentStatus newStatus)
    {
        var previous = Status;
        Status = newStatus;
        _domainEvents.Add(new AppointmentStatusChangedEvent(Id, previous, newStatus));
        UpdateTimestamp();
    }

    public void ClearDomainEvents() => _domainEvents.Clear();
}
