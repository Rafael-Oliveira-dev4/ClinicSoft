using ClinicSoft.Domain.Common;
using ClinicSoft.Domain.Interfaces;
using ClinicSoft.Domain.Model.Enums;
using ClinicSoft.Domain.Model.ValueObjects;

namespace ClinicSoft.Domain.Model;

public class Bill : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public Guid AppointmentId { get; set; }
    public Money GrossAmount { get; set; } = new(0);
    public Money InsuranceCoverage { get; set; } = new(0);
    public Money PatientAmount { get; set; } = new(0);
    public BillStatus Status { get; set; } = BillStatus.Pending;
    public DateTime DueDate { get; set; }
    public DateTime? PaidAt { get; set; }
    public string? Notes { get; set; }

    // Navigation
    public Patient? Patient { get; set; }
    public Appointment? Appointment { get; set; }

    public void MarkAsPaid()
    {
        Status = BillStatus.Paid;
        PaidAt = DateTime.UtcNow;
        UpdateTimestamp();
    }
}
