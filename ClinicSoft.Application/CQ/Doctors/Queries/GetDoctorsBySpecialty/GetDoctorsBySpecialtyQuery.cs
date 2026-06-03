using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Doctors.Common;
using ClinicSoft.Domain.Model.Enums;
using MediatR;

namespace ClinicSoft.Application.CQ.Doctors.Queries.GetDoctorsBySpecialty;

public class GetDoctorsBySpecialtyQuery : IRequest<Result<List<DoctorResponse>, Success, Error>>
{
    public MedicalSpecialty Specialty { get; init; }
}
