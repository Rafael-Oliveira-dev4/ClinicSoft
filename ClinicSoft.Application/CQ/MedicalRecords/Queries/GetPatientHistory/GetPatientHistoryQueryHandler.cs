using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.MedicalRecords.Common;
using ClinicSoft.Domain.Core.Uow;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.MedicalRecords.Queries.GetPatientHistory;

public class GetPatientHistoryQueryHandler : IRequestHandler<GetPatientHistoryQuery, Result<List<MedicalRecordResponse>, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetPatientHistoryQueryHandler> _logger;

    public GetPatientHistoryQueryHandler(IUnitOfWork unitOfWork, ILogger<GetPatientHistoryQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<List<MedicalRecordResponse>, Success, Error>> Handle(GetPatientHistoryQuery request, CancellationToken cancellationToken)
    {
        var patient = await _unitOfWork.PatientRepository.GetByIdAsync(request.PatientId);
        if (patient == null) return Error.PatientNotFound;

        var records = await _unitOfWork.MedicalRecordRepository.GetByPatientAsync(request.PatientId);

        return records.Select(r => new MedicalRecordResponse
        {
            Id = r.Id, PatientId = r.PatientId, AppointmentId = r.AppointmentId,
            Diagnosis = r.Diagnosis, ClinicalNotes = r.ClinicalNotes, TreatmentPlan = r.TreatmentPlan,
            Prescriptions = r.Prescriptions.Select(p => new PrescriptionResponse
            {
                Id = p.Id, MedicationName = p.MedicationName, Dosage = p.Dosage,
                Frequency = p.Frequency, Duration = p.Duration,
                Instructions = p.Instructions, ValidUntil = p.ValidUntil
            }).ToList(),
            CreatedAt = r.CreatedAt
        }).ToList();
    }
}
