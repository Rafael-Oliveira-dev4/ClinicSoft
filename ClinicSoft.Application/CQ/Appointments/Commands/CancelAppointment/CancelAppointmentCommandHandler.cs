using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Domain.Core.Uow;
using ClinicSoft.Domain.Model.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.Appointments.Commands.CancelAppointment;

public class CancelAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommand, Result<Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CancelAppointmentCommandHandler> _logger;

    public CancelAppointmentCommandHandler(IUnitOfWork unitOfWork, ILogger<CancelAppointmentCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<Success, Error>> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = await _unitOfWork.AppointmentRepository.GetByIdAsync(request.Id);
        if (appointment == null) return Error.AppointmentNotFound;

        appointment.CancellationReason = request.Reason;
        appointment.UpdateStatus(AppointmentStatus.Cancelled);
        await _unitOfWork.AppointmentRepository.UpdateAsync(appointment);
        await _unitOfWork.CommitAsync(cancellationToken);

        _logger.LogInformation("Consulta cancelada: {AppointmentId}", request.Id);
        return Success.Ok;
    }
}
