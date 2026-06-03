using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Patients.Common;
using MediatR;

namespace ClinicSoft.Application.CQ.Patients.Queries.GetPatientById;

public class GetPatientByIdQuery : IRequest<Result<PatientResponse, Success, Error>>
{
    public Guid Id { get; init; }
}
