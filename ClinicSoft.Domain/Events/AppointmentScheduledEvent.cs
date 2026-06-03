namespace ClinicSoft.Domain.Events;

public class AppointmentScheduledEvent : DomainEvent
{
    public Guid AppointmentId { get; }
    public Guid PatientId { get; }
    public Guid DoctorId { get; }
    public DateTime ScheduledAt { get; }

    public AppointmentScheduledEvent(Guid appointmentId, Guid patientId, Guid doctorId, DateTime scheduledAt)
    {
        AppointmentId = appointmentId;
        PatientId = patientId;
        DoctorId = doctorId;
        ScheduledAt = scheduledAt;
    }
}
