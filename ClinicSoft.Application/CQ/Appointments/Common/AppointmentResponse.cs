using ClinicSoft.Domain.Model.Enums;

namespace ClinicSoft.Application.CQ.Appointments.Common;

public class AppointmentResponse
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public required string PatientName { get; set; }
    public Guid DoctorId { get; set; }
    public required string DoctorName { get; set; }
    public Guid? RoomId { get; set; }
    public DateTime ScheduledAt { get; set; }
    public int DurationMinutes { get; set; }
    public AppointmentStatus Status { get; set; }
    public AppointmentType Type { get; set; }
    public string? Notes { get; set; }
    public string? CancellationReason { get; set; }
    public decimal? ConsultationFee { get; set; }
    public DateTime? CreatedAt { get; set; }
}
