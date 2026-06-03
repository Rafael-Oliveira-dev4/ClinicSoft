using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Medications.Common;
using ClinicSoft.Domain.Core.Uow;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.Medications.Queries.GetLowStockMedications;

public class GetLowStockMedicationsQueryHandler : IRequestHandler<GetLowStockMedicationsQuery, Result<List<MedicationResponse>, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetLowStockMedicationsQueryHandler> _logger;

    public GetLowStockMedicationsQueryHandler(IUnitOfWork unitOfWork, ILogger<GetLowStockMedicationsQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<List<MedicationResponse>, Success, Error>> Handle(GetLowStockMedicationsQuery request, CancellationToken cancellationToken)
    {
        var medications = await _unitOfWork.MedicationRepository.GetLowStockAsync();

        return medications.Select(m => new MedicationResponse
        {
            Id = m.Id, Name = m.Name, ActiveSubstance = m.ActiveSubstance,
            Category = m.Category, Unit = m.Unit, CurrentStock = m.CurrentStock,
            MinimumStock = m.MinimumStock, IsLowStock = m.IsLowStock,
            UnitPrice = m.UnitPrice?.Amount, RequiresPrescription = m.RequiresPrescription,
            CreatedAt = m.CreatedAt
        }).ToList();
    }
}
