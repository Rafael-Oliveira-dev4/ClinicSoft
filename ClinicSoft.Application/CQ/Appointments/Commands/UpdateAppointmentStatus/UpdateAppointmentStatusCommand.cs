using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Domain.Model.Enums;
using MediatR;

namespace ClinicSoft.Application.CQ.Appointments.Commands.UpdateAppointmentStatus;

public class UpdateAppointmentStatusCommand : IRequest<Result<Success, Error>>
{
    public Guid Id { get; init; }
    public AppointmentStatus NewStatus { get; init; }
}
