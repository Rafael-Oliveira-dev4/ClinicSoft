using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Appointments.Common;
using ClinicSoft.Domain.Model.Enums;
using MediatR;

namespace ClinicSoft.Application.CQ.Appointments.Queries.GetAppointments;

public class GetAppointmentsQuery : IRequest<Result<List<AppointmentResponse>, Success, Error>>
{
    public AppointmentStatus? Status { get; init; }
    public Guid? DoctorId { get; init; }
    public Guid? PatientId { get; init; }
    public DateTime? From { get; init; }
    public DateTime? To { get; init; }
}
