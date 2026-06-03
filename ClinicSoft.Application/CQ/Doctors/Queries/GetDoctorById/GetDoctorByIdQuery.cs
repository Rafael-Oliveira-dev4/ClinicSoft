using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Doctors.Common;
using MediatR;

namespace ClinicSoft.Application.CQ.Doctors.Queries.GetDoctorById;

public class GetDoctorByIdQuery : IRequest<Result<DoctorResponse, Success, Error>>
{
    public Guid Id { get; init; }
}
