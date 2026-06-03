using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.MedicalRecords.Common;
using ClinicSoft.Domain.Core.Uow;
using ClinicSoft.Domain.Model;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.MedicalRecords.Commands.CreateMedicalRecord;

public class CreateMedicalRecordCommandHandler : IRequestHandler<CreateMedicalRecordCommand, Result<MedicalRecordResponse, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateMedicalRecordCommandHandler> _logger;

    public CreateMedicalRecordCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateMedicalRecordCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<MedicalRecordResponse, Success, Error>> Handle(CreateMedicalRecordCommand request, CancellationToken cancellationToken)
    {
        var existing = await _unitOfWork.MedicalRecordRepository.GetByAppointmentAsync(request.AppointmentId);
        if (existing != null) return Error.MedicalRecordAlreadyExists;

        var appointment = await _unitOfWork.AppointmentRepository.GetByIdAsync(request.AppointmentId);
        if (appointment == null) return Error.AppointmentNotFound;

        var record = new MedicalRecord
        {
            PatientId = request.PatientId,
            AppointmentId = request.AppointmentId,
            Diagnosis = request.Diagnosis,
            ClinicalNotes = request.ClinicalNotes,
            TreatmentPlan = request.TreatmentPlan
        };

        var saved = await _unitOfWork.MedicalRecordRepository.SaveAsync(record);
        await _unitOfWork.CommitAsync(cancellationToken);

        _logger.LogInformation("Registo clínico criado: {RecordId}", saved.Id);

        return new MedicalRecordResponse
        {
            Id = saved.Id, PatientId = saved.PatientId, AppointmentId = saved.AppointmentId,
            Diagnosis = saved.Diagnosis, ClinicalNotes = saved.ClinicalNotes,
            TreatmentPlan = saved.TreatmentPlan, CreatedAt = saved.CreatedAt
        };
    }
}
