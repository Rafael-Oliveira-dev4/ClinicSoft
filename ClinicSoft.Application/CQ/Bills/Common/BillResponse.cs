using ClinicSoft.Domain.Model.Enums;

namespace ClinicSoft.Application.CQ.Bills.Common;

public class BillResponse
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public Guid AppointmentId { get; set; }
    public decimal GrossAmount { get; set; }
    public decimal InsuranceCoverage { get; set; }
    public decimal PatientAmount { get; set; }
    public BillStatus Status { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? PaidAt { get; set; }
    public string? Notes { get; set; }
    public DateTime? CreatedAt { get; set; }
}
