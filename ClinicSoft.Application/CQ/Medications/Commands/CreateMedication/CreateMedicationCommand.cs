using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Medications.Common;
using ClinicSoft.Domain.Model.Enums;
using MediatR;

namespace ClinicSoft.Application.CQ.Medications.Commands.CreateMedication;

public class CreateMedicationCommand : IRequest<Result<MedicationResponse, Success, Error>>
{
    public required string Name { get; init; }
    public required string ActiveSubstance { get; init; }
    public MedicationCategory Category { get; init; }
    public required string Unit { get; init; }
    public int CurrentStock { get; init; }
    public int MinimumStock { get; init; }
    public decimal? UnitPrice { get; init; }
    public bool RequiresPrescription { get; init; } = true;
}
