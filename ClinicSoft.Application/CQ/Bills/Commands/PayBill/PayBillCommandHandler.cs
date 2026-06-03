using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Domain.Core.Uow;
using ClinicSoft.Domain.Model.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.Bills.Commands.PayBill;

public class PayBillCommandHandler : IRequestHandler<PayBillCommand, Result<Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<PayBillCommandHandler> _logger;

    public PayBillCommandHandler(IUnitOfWork unitOfWork, ILogger<PayBillCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<Success, Error>> Handle(PayBillCommand request, CancellationToken cancellationToken)
    {
        var bill = await _unitOfWork.BillRepository.GetByIdAsync(request.Id);
        if (bill == null) return Error.BillNotFound;
        if (bill.Status == BillStatus.Paid) return Error.BillAlreadyPaid;

        bill.MarkAsPaid();
        await _unitOfWork.BillRepository.UpdateAsync(bill);
        await _unitOfWork.CommitAsync(cancellationToken);

        _logger.LogInformation("Fatura paga: {BillId}", request.Id);
        return Success.Ok;
    }
}
