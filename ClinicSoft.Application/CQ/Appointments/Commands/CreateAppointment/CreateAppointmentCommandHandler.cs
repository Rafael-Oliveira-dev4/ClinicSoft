using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Appointments.Common;
using ClinicSoft.Domain.Core.Uow;
using ClinicSoft.Domain.Events;
using ClinicSoft.Domain.Model;
using ClinicSoft.Domain.Model.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.Appointments.Commands.CreateAppointment;

public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, Result<AppointmentResponse, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateAppointmentCommandHandler> _logger;

    public CreateAppointmentCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateAppointmentCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<AppointmentResponse, Success, Error>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var patient = await _unitOfWork.PatientRepository.GetByIdAsync(request.PatientId);
        if (patient == null) return Error.PatientNotFound;

        var doctor = await _unitOfWork.DoctorRepository.GetByIdAsync(request.DoctorId);
        if (doctor == null) return Error.DoctorNotFound;

        var conflicts = await _unitOfWork.AppointmentRepository.GetByDoctorAndDateAsync(
            request.DoctorId, DateOnly.FromDateTime(request.ScheduledAt));

        var hasConflict = conflicts.Any(a =>
            a.ScheduledAt < request.ScheduledAt.AddMinutes(request.DurationMinutes) &&
            request.ScheduledAt < a.ScheduledAt.AddMinutes(a.DurationMinutes));

        if (hasConflict) return Error.AppointmentConflict;

        var appointment = new Appointment
        {
            PatientId = request.PatientId,
            DoctorId = request.DoctorId,
            RoomId = request.RoomId,
            ScheduledAt = request.ScheduledAt,
            DurationMinutes = request.DurationMinutes,
            Type = request.Type,
            Notes = request.Notes,
            ConsultationFee = request.ConsultationFee.HasValue ? new Money(request.ConsultationFee.Value) : null
        };

        var saved = await _unitOfWork.AppointmentRepository.SaveAsync(appointment);
        await _unitOfWork.CommitAsync(cancellationToken);

        _logger.LogInformation("Consulta agendada: {AppointmentId}", saved.Id);

        return new AppointmentResponse
        {
            Id = saved.Id, PatientId = saved.PatientId, PatientName = patient.Name.FullName,
            DoctorId = saved.DoctorId, DoctorName = doctor.Name.FullName, RoomId = saved.RoomId,
            ScheduledAt = saved.ScheduledAt, DurationMinutes = saved.DurationMinutes,
            Status = saved.Status, Type = saved.Type, Notes = saved.Notes,
            ConsultationFee = saved.ConsultationFee?.Amount, CreatedAt = saved.CreatedAt
        };
    }
}
