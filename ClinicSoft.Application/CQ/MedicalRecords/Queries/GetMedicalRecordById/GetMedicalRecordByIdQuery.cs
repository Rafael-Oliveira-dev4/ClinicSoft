using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.MedicalRecords.Common;
using MediatR;

namespace ClinicSoft.Application.CQ.MedicalRecords.Queries.GetMedicalRecordById;

public class GetMedicalRecordByIdQuery : IRequest<Result<MedicalRecordResponse, Success, Error>>
{
    public Guid Id { get; init; }
}
