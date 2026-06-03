using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.MedicalRecords.Common;
using MediatR;

namespace ClinicSoft.Application.CQ.MedicalRecords.Queries.GetPatientHistory;

public class GetPatientHistoryQuery : IRequest<Result<List<MedicalRecordResponse>, Success, Error>>
{
    public Guid PatientId { get; init; }
}
