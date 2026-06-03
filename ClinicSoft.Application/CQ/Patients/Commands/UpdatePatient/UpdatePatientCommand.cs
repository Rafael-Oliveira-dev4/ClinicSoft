using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Patients.Common;
using ClinicSoft.Domain.Model.Enums;
using MediatR;

namespace ClinicSoft.Application.CQ.Patients.Commands.UpdatePatient;

public class UpdatePatientCommand : IRequest<Result<PatientResponse, Success, Error>>
{
    public Guid Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public string? Phone { get; init; }
    public DateTime DateOfBirth { get; init; }
    public Gender Gender { get; init; }
    public BloodType BloodType { get; init; }
    public string? Allergies { get; init; }
    public string? ChronicConditions { get; init; }
    public Guid? InsurancePlanId { get; init; }
}
