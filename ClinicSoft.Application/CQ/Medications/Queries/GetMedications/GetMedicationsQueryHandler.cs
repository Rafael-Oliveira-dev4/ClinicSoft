using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Medications.Common;
using ClinicSoft.Domain.Core.Uow;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.Medications.Queries.GetMedications;

public class GetMedicationsQueryHandler : IRequestHandler<GetMedicationsQuery, Result<List<MedicationResponse>, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetMedicationsQueryHandler> _logger;

    public GetMedicationsQueryHandler(IUnitOfWork unitOfWork, ILogger<GetMedicationsQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<List<MedicationResponse>, Success, Error>> Handle(GetMedicationsQuery request, CancellationToken cancellationToken)
    {
        var medications = string.IsNullOrWhiteSpace(request.SearchTerm)
            ? await _unitOfWork.MedicationRepository.GetAllAsync()
            : await _unitOfWork.MedicationRepository.SearchByNameAsync(request.SearchTerm);

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
