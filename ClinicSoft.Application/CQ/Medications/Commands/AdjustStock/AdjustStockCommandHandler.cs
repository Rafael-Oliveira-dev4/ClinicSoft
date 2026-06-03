using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Domain.Core.Uow;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.Medications.Commands.AdjustStock;

public class AdjustStockCommandHandler : IRequestHandler<AdjustStockCommand, Result<Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AdjustStockCommandHandler> _logger;

    public AdjustStockCommandHandler(IUnitOfWork unitOfWork, ILogger<AdjustStockCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<Success, Error>> Handle(AdjustStockCommand request, CancellationToken cancellationToken)
    {
        var medication = await _unitOfWork.MedicationRepository.GetByIdAsync(request.MedicationId);
        if (medication == null) return Error.MedicationNotFound;

        if (medication.CurrentStock + request.Quantity < 0) return Error.InsufficientStock;

        medication.AdjustStock(request.Quantity);
        await _unitOfWork.MedicationRepository.UpdateAsync(medication);
        await _unitOfWork.CommitAsync(cancellationToken);

        _logger.LogInformation("Stock ajustado {MedicationId}: {Qty}", request.MedicationId, request.Quantity);
        return Success.Ok;
    }
}
