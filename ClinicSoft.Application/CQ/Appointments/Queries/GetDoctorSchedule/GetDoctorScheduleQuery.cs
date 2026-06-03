using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Appointments.Common;
using MediatR;

namespace ClinicSoft.Application.CQ.Appointments.Queries.GetDoctorSchedule;

public class GetDoctorScheduleQuery : IRequest<Result<List<AppointmentResponse>, Success, Error>>
{
    public Guid DoctorId { get; init; }
    public DateOnly Date { get; init; }
}
