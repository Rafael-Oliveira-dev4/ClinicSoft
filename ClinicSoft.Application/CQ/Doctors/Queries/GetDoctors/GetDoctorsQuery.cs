using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Doctors.Common;
using MediatR;

namespace ClinicSoft.Application.CQ.Doctors.Queries.GetDoctors;

public class GetDoctorsQuery : IRequest<Result<List<DoctorResponse>, Success, Error>>
{
    public string? SearchTerm { get; init; }
}
