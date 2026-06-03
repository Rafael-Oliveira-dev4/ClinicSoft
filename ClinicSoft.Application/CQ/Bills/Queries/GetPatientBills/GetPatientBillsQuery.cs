using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Bills.Common;
using MediatR;

namespace ClinicSoft.Application.CQ.Bills.Queries.GetPatientBills;

public class GetPatientBillsQuery : IRequest<Result<List<BillResponse>, Success, Error>>
{
    public Guid PatientId { get; init; }
}
