using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Doctors.Common;
using ClinicSoft.Domain.Model.Enums;
using MediatR;

namespace ClinicSoft.Application.CQ.Doctors.Commands.CreateDoctor;

public class CreateDoctorCommand : IRequest<Result<DoctorResponse, Success, Error>>
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public string? Phone { get; init; }
    public required string CedulaProfissional { get; init; }
    public MedicalSpecialty Specialty { get; init; }
    public string? Bio { get; init; }
}
