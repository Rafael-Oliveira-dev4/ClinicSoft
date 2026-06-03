using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.MedicalRecords.Common;
using ClinicSoft.Domain.Core.Uow;
using ClinicSoft.Domain.Model;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.MedicalRecords.Commands.AddPrescription;

public class AddPrescriptionCommandHandler : IRequestHandler<AddPrescriptionCommand, Result<PrescriptionResponse, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddPrescriptionCommandHandler> _logger;

    public AddPrescriptionCommandHandler(IUnitOfWork unitOfWork, ILogger<AddPrescriptionCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<PrescriptionResponse, Success, Error>> Handle(AddPrescriptionCommand request, CancellationToken cancellationToken)
    {
        var record = await _unitOfWork.MedicalRecordRepository.GetByIdAsync(request.MedicalRecordId);
        if (record == null) return Error.MedicalRecordNotFound;

        var prescription = new Prescription
        {
            MedicalRecordId = request.MedicalRecordId,
            MedicationName = request.MedicationName,
            Dosage = request.Dosage,
            Frequency = request.Frequency,
            Duration = request.Duration,
            Instructions = request.Instructions,
            ValidUntil = request.ValidUntil
        };

        record.Prescriptions.Add(prescription);
        await _unitOfWork.MedicalRecordRepository.UpdateAsync(record);
        await _unitOfWork.CommitAsync(cancellationToken);

        _logger.LogInformation("Prescrição adicionada ao registo {RecordId}", request.MedicalRecordId);

        return new PrescriptionResponse
        {
            Id = prescription.Id, MedicationName = prescription.MedicationName,
            Dosage = prescription.Dosage, Frequency = prescription.Frequency,
            Duration = prescription.Duration, Instructions = prescription.Instructions,
            ValidUntil = prescription.ValidUntil
        };
    }
}
