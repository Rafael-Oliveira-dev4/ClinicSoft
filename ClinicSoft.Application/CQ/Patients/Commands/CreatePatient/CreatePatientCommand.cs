using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Patients.Common;
using ClinicSoft.Domain.Model.Enums;
using MediatR;

namespace ClinicSoft.Application.CQ.Patients.Commands.CreatePatient;

public class CreatePatientCommand : IRequest<Result<PatientResponse, Success, Error>>
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public string? Phone { get; init; }
    public DateTime DateOfBirth { get; init; }
    public Gender Gender { get; init; }
    public BloodType BloodType { get; init; }
    public string? Nif { get; init; }
    public string? CartaoCidadao { get; init; }
    public string? NumeroUtenteSns { get; init; }
    public string? Allergies { get; init; }
    public string? ChronicConditions { get; init; }
    public Guid? InsurancePlanId { get; init; }
}
