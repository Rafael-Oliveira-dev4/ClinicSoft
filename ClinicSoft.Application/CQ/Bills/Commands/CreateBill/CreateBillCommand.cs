using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Bills.Common;
using MediatR;

namespace ClinicSoft.Application.CQ.Bills.Commands.CreateBill;

public class CreateBillCommand : IRequest<Result<BillResponse, Success, Error>>
{
    public Guid PatientId { get; init; }
    public Guid AppointmentId { get; init; }
    public decimal GrossAmount { get; init; }
    public decimal InsuranceCoverage { get; init; }
    public DateTime DueDate { get; init; }
    public string? Notes { get; init; }
}
