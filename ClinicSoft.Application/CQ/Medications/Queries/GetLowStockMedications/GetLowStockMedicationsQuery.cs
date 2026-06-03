using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Medications.Common;
using MediatR;

namespace ClinicSoft.Application.CQ.Medications.Queries.GetLowStockMedications;

public class GetLowStockMedicationsQuery : IRequest<Result<List<MedicationResponse>, Success, Error>>;
