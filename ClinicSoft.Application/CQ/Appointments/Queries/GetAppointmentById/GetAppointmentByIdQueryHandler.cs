using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Appointments.Common;
using ClinicSoft.Domain.Core.Uow;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.Appointments.Queries.GetAppointmentById;

public class GetAppointmentByIdQueryHandler : IRequestHandler<GetAppointmentByIdQuery, Result<AppointmentResponse, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAppointmentByIdQueryHandler> _logger;

    public GetAppointmentByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetAppointmentByIdQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<AppointmentResponse, Success, Error>> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
    {
        var a = await _unitOfWork.AppointmentRepository.GetByIdAsync(request.Id);
        if (a == null) return Error.AppointmentNotFound;

        return new AppointmentResponse
        {
            Id = a.Id, PatientId = a.PatientId, PatientName = a.Patient?.Name.FullName ?? "",
            DoctorId = a.DoctorId, DoctorName = a.Doctor?.Name.FullName ?? "",
            RoomId = a.RoomId, ScheduledAt = a.ScheduledAt, DurationMinutes = a.DurationMinutes,
            Status = a.Status, Type = a.Type, Notes = a.Notes,
            CancellationReason = a.CancellationReason,
            ConsultationFee = a.ConsultationFee?.Amount, CreatedAt = a.CreatedAt
        };
    }
}
