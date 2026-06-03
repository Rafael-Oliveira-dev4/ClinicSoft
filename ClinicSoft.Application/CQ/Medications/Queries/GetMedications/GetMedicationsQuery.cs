using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Medications.Common;
using MediatR;

namespace ClinicSoft.Application.CQ.Medications.Queries.GetMedications;

public class GetMedicationsQuery : IRequest<Result<List<MedicationResponse>, Success, Error>>
{
    public string? SearchTerm { get; init; }
}
