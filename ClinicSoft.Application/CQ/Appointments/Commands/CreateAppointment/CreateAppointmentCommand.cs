using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Appointments.Common;
using ClinicSoft.Domain.Model.Enums;
using MediatR;

namespace ClinicSoft.Application.CQ.Appointments.Commands.CreateAppointment;

public class CreateAppointmentCommand : IRequest<Result<AppointmentResponse, Success, Error>>
{
    public Guid PatientId { get; init; }
    public Guid DoctorId { get; init; }
    public Guid? RoomId { get; init; }
    public DateTime ScheduledAt { get; init; }
    public int DurationMinutes { get; init; } = 30;
    public AppointmentType Type { get; init; }
    public string? Notes { get; init; }
    public decimal? ConsultationFee { get; init; }
}
