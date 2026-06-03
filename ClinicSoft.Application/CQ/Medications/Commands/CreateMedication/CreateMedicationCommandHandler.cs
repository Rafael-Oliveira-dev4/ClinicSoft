using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Medications.Common;
using ClinicSoft.Domain.Core.Uow;
using ClinicSoft.Domain.Model;
using ClinicSoft.Domain.Model.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.Medications.Commands.CreateMedication;

public class CreateMedicationCommandHandler : IRequestHandler<CreateMedicationCommand, Result<MedicationResponse, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateMedicationCommandHandler> _logger;

    public CreateMedicationCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateMedicationCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<MedicationResponse, Success, Error>> Handle(CreateMedicationCommand request, CancellationToken cancellationToken)
    {
        var medication = new Medication
        {
            Name = request.Name,
            ActiveSubstance = request.ActiveSubstance,
            Category = request.Category,
            Unit = request.Unit,
            CurrentStock = request.CurrentStock,
            MinimumStock = request.MinimumStock,
            UnitPrice = request.UnitPrice.HasValue ? new Money(request.UnitPrice.Value) : null,
            RequiresPrescription = request.RequiresPrescription
        };

        var saved = await _unitOfWork.MedicationRepository.SaveAsync(medication);
        await _unitOfWork.CommitAsync(cancellationToken);

        _logger.LogInformation("Medicamento criado: {MedicationId}", saved.Id);

        return new MedicationResponse
        {
            Id = saved.Id, Name = saved.Name, ActiveSubstance = saved.ActiveSubstance,
            Category = saved.Category, Unit = saved.Unit, CurrentStock = saved.CurrentStock,
            MinimumStock = saved.MinimumStock, IsLowStock = saved.IsLowStock,
            UnitPrice = saved.UnitPrice?.Amount, RequiresPrescription = saved.RequiresPrescription,
            CreatedAt = saved.CreatedAt
        };
    }
}
