using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.MedicalRecords.Common;
using MediatR;

namespace ClinicSoft.Application.CQ.MedicalRecords.Commands.CreateMedicalRecord;

public class CreateMedicalRecordCommand : IRequest<Result<MedicalRecordResponse, Success, Error>>
{
    public Guid PatientId { get; init; }
    public Guid AppointmentId { get; init; }
    public string? Diagnosis { get; init; }
    public string? ClinicalNotes { get; init; }
    public string? TreatmentPlan { get; init; }
}
