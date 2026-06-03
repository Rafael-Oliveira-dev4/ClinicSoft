using ClinicSoft.Domain.Model.Enums;

namespace ClinicSoft.Domain.Events;

public class AppointmentStatusChangedEvent : DomainEvent
{
    public Guid AppointmentId { get; }
    public AppointmentStatus PreviousStatus { get; }
    public AppointmentStatus NewStatus { get; }

    public AppointmentStatusChangedEvent(Guid appointmentId, AppointmentStatus previous, AppointmentStatus newStatus)
    {
        AppointmentId = appointmentId;
        PreviousStatus = previous;
        NewStatus = newStatus;
    }
}
