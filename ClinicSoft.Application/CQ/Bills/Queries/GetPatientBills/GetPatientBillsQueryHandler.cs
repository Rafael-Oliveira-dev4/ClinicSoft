using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Bills.Common;
using ClinicSoft.Domain.Core.Uow;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.Bills.Queries.GetPatientBills;

public class GetPatientBillsQueryHandler : IRequestHandler<GetPatientBillsQuery, Result<List<BillResponse>, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetPatientBillsQueryHandler> _logger;

    public GetPatientBillsQueryHandler(IUnitOfWork unitOfWork, ILogger<GetPatientBillsQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<List<BillResponse>, Success, Error>> Handle(GetPatientBillsQuery request, CancellationToken cancellationToken)
    {
        var patient = await _unitOfWork.PatientRepository.GetByIdAsync(request.PatientId);
        if (patient == null) return Error.PatientNotFound;

        var bills = await _unitOfWork.BillRepository.GetByPatientAsync(request.PatientId);

        return bills.Select(b => new BillResponse
        {
            Id = b.Id, PatientId = b.PatientId, AppointmentId = b.AppointmentId,
            GrossAmount = b.GrossAmount.Amount, InsuranceCoverage = b.InsuranceCoverage.Amount,
            PatientAmount = b.PatientAmount.Amount, Status = b.Status,
            DueDate = b.DueDate, PaidAt = b.PaidAt, Notes = b.Notes, CreatedAt = b.CreatedAt
        }).ToList();
    }
}
