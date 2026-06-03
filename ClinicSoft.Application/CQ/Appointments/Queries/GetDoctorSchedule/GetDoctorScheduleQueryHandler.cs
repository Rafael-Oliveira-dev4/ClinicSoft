using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Appointments.Common;
using ClinicSoft.Domain.Core.Uow;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.Appointments.Queries.GetDoctorSchedule;

public class GetDoctorScheduleQueryHandler : IRequestHandler<GetDoctorScheduleQuery, Result<List<AppointmentResponse>, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetDoctorScheduleQueryHandler> _logger;

    public GetDoctorScheduleQueryHandler(IUnitOfWork unitOfWork, ILogger<GetDoctorScheduleQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<List<AppointmentResponse>, Success, Error>> Handle(GetDoctorScheduleQuery request, CancellationToken cancellationToken)
    {
        var doctor = await _unitOfWork.DoctorRepository.GetByIdAsync(request.DoctorId);
        if (doctor == null) return Error.DoctorNotFound;

        var appointments = await _unitOfWork.AppointmentRepository.GetByDoctorAndDateAsync(request.DoctorId, request.Date);

        return appointments.OrderBy(a => a.ScheduledAt).Select(a => new AppointmentResponse
        {
            Id = a.Id, PatientId = a.PatientId, PatientName = a.Patient?.Name.FullName ?? "",
            DoctorId = a.DoctorId, DoctorName = doctor.Name.FullName,
            RoomId = a.RoomId, ScheduledAt = a.ScheduledAt, DurationMinutes = a.DurationMinutes,
            Status = a.Status, Type = a.Type, Notes = a.Notes, CreatedAt = a.CreatedAt
        }).ToList();
    }
}
