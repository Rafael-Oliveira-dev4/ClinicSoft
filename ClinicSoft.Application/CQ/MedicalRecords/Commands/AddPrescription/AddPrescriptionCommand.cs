using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.MedicalRecords.Common;
using MediatR;

namespace ClinicSoft.Application.CQ.MedicalRecords.Commands.AddPrescription;

public class AddPrescriptionCommand : IRequest<Result<PrescriptionResponse, Success, Error>>
{
    public Guid MedicalRecordId { get; init; }
    public required string MedicationName { get; init; }
    public required string Dosage { get; init; }
    public required string Frequency { get; init; }
    public required string Duration { get; init; }
    public string? Instructions { get; init; }
    public DateTime ValidUntil { get; init; }
}
