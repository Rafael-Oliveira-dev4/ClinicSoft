using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Appointments.Common;
using ClinicSoft.Domain.Core.Uow;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.Appointments.Queries.GetAppointments;

public class GetAppointmentsQueryHandler : IRequestHandler<GetAppointmentsQuery, Result<List<AppointmentResponse>, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAppointmentsQueryHandler> _logger;

    public GetAppointmentsQueryHandler(IUnitOfWork unitOfWork, ILogger<GetAppointmentsQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<List<AppointmentResponse>, Success, Error>> Handle(GetAppointmentsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Domain.Model.Appointment> appointments;

        if (request.DoctorId.HasValue)
            appointments = await _unitOfWork.AppointmentRepository.GetByDoctorAsync(request.DoctorId.Value);
        else if (request.PatientId.HasValue)
            appointments = await _unitOfWork.AppointmentRepository.GetByPatientAsync(request.PatientId.Value);
        else if (request.Status.HasValue)
            appointments = await _unitOfWork.AppointmentRepository.GetByStatusAsync(request.Status.Value);
        else
            appointments = await _unitOfWork.AppointmentRepository.GetAllAsync();

        if (request.From.HasValue) appointments = appointments.Where(a => a.ScheduledAt >= request.From.Value);
        if (request.To.HasValue) appointments = appointments.Where(a => a.ScheduledAt <= request.To.Value);

        return appointments.Select(a => new AppointmentResponse
        {
            Id = a.Id, PatientId = a.PatientId, PatientName = a.Patient?.Name.FullName ?? "",
            DoctorId = a.DoctorId, DoctorName = a.Doctor?.Name.FullName ?? "",
            RoomId = a.RoomId, ScheduledAt = a.ScheduledAt, DurationMinutes = a.DurationMinutes,
            Status = a.Status, Type = a.Type, Notes = a.Notes,
            CancellationReason = a.CancellationReason,
            ConsultationFee = a.ConsultationFee?.Amount, CreatedAt = a.CreatedAt
        }).ToList();
    }
}
