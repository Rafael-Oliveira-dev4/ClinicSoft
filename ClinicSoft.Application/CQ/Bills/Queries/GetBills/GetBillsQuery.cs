using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Bills.Common;
using ClinicSoft.Domain.Model.Enums;
using MediatR;

namespace ClinicSoft.Application.CQ.Bills.Queries.GetBills;

public class GetBillsQuery : IRequest<Result<List<BillResponse>, Success, Error>>
{
    public BillStatus? Status { get; init; }
}
