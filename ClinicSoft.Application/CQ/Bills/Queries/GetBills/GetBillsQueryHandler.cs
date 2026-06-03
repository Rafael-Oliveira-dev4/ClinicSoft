using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Bills.Common;
using ClinicSoft.Domain.Core.Uow;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.Bills.Queries.GetBills;

public class GetBillsQueryHandler : IRequestHandler<GetBillsQuery, Result<List<BillResponse>, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetBillsQueryHandler> _logger;

    public GetBillsQueryHandler(IUnitOfWork unitOfWork, ILogger<GetBillsQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<List<BillResponse>, Success, Error>> Handle(GetBillsQuery request, CancellationToken cancellationToken)
    {
        var bills = request.Status.HasValue
            ? await _unitOfWork.BillRepository.GetByStatusAsync(request.Status.Value)
            : await _unitOfWork.BillRepository.GetAllAsync();

        return bills.Select(b => new BillResponse
        {
            Id = b.Id, PatientId = b.PatientId, AppointmentId = b.AppointmentId,
            GrossAmount = b.GrossAmount.Amount, InsuranceCoverage = b.InsuranceCoverage.Amount,
            PatientAmount = b.PatientAmount.Amount, Status = b.Status,
            DueDate = b.DueDate, PaidAt = b.PaidAt, Notes = b.Notes, CreatedAt = b.CreatedAt
        }).ToList();
    }
}
