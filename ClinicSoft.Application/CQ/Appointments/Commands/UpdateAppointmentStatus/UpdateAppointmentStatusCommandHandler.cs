using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Domain.Core.Uow;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.Appointments.Commands.UpdateAppointmentStatus;

public class UpdateAppointmentStatusCommandHandler : IRequestHandler<UpdateAppointmentStatusCommand, Result<Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateAppointmentStatusCommandHandler> _logger;

    public UpdateAppointmentStatusCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateAppointmentStatusCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<Success, Error>> Handle(UpdateAppointmentStatusCommand request, CancellationToken cancellationToken)
    {
        var appointment = await _unitOfWork.AppointmentRepository.GetByIdAsync(request.Id);
        if (appointment == null) return Error.AppointmentNotFound;

        appointment.UpdateStatus(request.NewStatus);
        await _unitOfWork.AppointmentRepository.UpdateAsync(appointment);
        await _unitOfWork.CommitAsync(cancellationToken);

        _logger.LogInformation("Estado da consulta {AppointmentId} → {Status}", request.Id, request.NewStatus);
        return Success.Ok;
    }
}
