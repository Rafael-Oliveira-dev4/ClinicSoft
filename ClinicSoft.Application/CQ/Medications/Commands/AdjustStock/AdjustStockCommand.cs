using ClinicSoft.Application.Common.Responses;
using MediatR;

namespace ClinicSoft.Application.CQ.Medications.Commands.AdjustStock;

public class AdjustStockCommand : IRequest<Result<Success, Error>>
{
    public Guid MedicationId { get; init; }
    public int Quantity { get; init; }
}
