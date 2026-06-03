using ClinicSoft.Application.Common.Responses;
using MediatR;

namespace ClinicSoft.Application.CQ.Bills.Commands.PayBill;

public class PayBillCommand : IRequest<Result<Success, Error>>
{
    public Guid Id { get; init; }
}
