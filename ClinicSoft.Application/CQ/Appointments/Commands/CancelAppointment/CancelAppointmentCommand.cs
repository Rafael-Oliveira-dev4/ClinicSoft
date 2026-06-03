using ClinicSoft.Application.Common.Responses;
using MediatR;

namespace ClinicSoft.Application.CQ.Appointments.Commands.CancelAppointment;

public class CancelAppointmentCommand : IRequest<Result<Success, Error>>
{
    public Guid Id { get; init; }
    public string? Reason { get; init; }
}
