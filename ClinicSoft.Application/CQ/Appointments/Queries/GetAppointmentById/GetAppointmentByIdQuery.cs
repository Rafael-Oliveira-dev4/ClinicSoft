using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Appointments.Common;
using MediatR;

namespace ClinicSoft.Application.CQ.Appointments.Queries.GetAppointmentById;

public class GetAppointmentByIdQuery : IRequest<Result<AppointmentResponse, Success, Error>>
{
    public Guid Id { get; init; }
}
