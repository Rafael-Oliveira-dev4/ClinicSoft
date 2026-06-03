using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Patients.Common;
using MediatR;

namespace ClinicSoft.Application.CQ.Patients.Queries.GetPatients;

public class GetPatientsQuery : IRequest<Result<GetPatientsResponse, Success, Error>>
{
    public string? SearchTerm { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 20;
}

public class GetPatientsResponse
{
    public List<PatientResponse> Patients { get; set; } = [];
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
}
