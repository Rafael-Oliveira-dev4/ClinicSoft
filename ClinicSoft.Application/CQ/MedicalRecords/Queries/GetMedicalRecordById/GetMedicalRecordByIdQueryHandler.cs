using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.MedicalRecords.Common;
using ClinicSoft.Domain.Core.Uow;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.MedicalRecords.Queries.GetMedicalRecordById;

public class GetMedicalRecordByIdQueryHandler : IRequestHandler<GetMedicalRecordByIdQuery, Result<MedicalRecordResponse, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetMedicalRecordByIdQueryHandler> _logger;

    public GetMedicalRecordByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetMedicalRecordByIdQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<MedicalRecordResponse, Success, Error>> Handle(GetMedicalRecordByIdQuery request, CancellationToken cancellationToken)
    {
        var r = await _unitOfWork.MedicalRecordRepository.GetByIdAsync(request.Id);
        if (r == null) return Error.MedicalRecordNotFound;

        return new MedicalRecordResponse
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
        };
    }
}
