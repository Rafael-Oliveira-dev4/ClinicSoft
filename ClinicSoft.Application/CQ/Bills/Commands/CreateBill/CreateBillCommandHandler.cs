using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Bills.Common;
using ClinicSoft.Domain.Core.Uow;
using ClinicSoft.Domain.Model;
using ClinicSoft.Domain.Model.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.Bills.Commands.CreateBill;

public class CreateBillCommandHandler : IRequestHandler<CreateBillCommand, Result<BillResponse, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateBillCommandHandler> _logger;

    public CreateBillCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateBillCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<BillResponse, Success, Error>> Handle(CreateBillCommand request, CancellationToken cancellationToken)
    {
        var patient = await _unitOfWork.PatientRepository.GetByIdAsync(request.PatientId);
        if (patient == null) return Error.PatientNotFound;

        var patientAmount = request.GrossAmount - request.InsuranceCoverage;

        var bill = new Bill
        {
            PatientId = request.PatientId,
            AppointmentId = request.AppointmentId,
            GrossAmount = new Money(request.GrossAmount),
            InsuranceCoverage = new Money(request.InsuranceCoverage),
            PatientAmount = new Money(patientAmount < 0 ? 0 : patientAmount),
            DueDate = request.DueDate,
            Notes = request.Notes
        };

        var saved = await _unitOfWork.BillRepository.SaveAsync(bill);
        await _unitOfWork.CommitAsync(cancellationToken);

        _logger.LogInformation("Fatura criada: {BillId}", saved.Id);

        return new BillResponse
        {
            Id = saved.Id, PatientId = saved.PatientId, AppointmentId = saved.AppointmentId,
            GrossAmount = saved.GrossAmount.Amount, InsuranceCoverage = saved.InsuranceCoverage.Amount,
            PatientAmount = saved.PatientAmount.Amount, Status = saved.Status,
            DueDate = saved.DueDate, Notes = saved.Notes, CreatedAt = saved.CreatedAt
        };
    }
}
