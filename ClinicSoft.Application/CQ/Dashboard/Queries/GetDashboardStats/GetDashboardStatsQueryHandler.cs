using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Domain.Core.Uow;
using ClinicSoft.Domain.Model.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.Dashboard.Queries.GetDashboardStats;

public class GetDashboardStatsQueryHandler : IRequestHandler<GetDashboardStatsQuery, Result<DashboardStatsResponse, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetDashboardStatsQueryHandler> _logger;

    public GetDashboardStatsQueryHandler(IUnitOfWork unitOfWork, ILogger<GetDashboardStatsQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<DashboardStatsResponse, Success, Error>> Handle(GetDashboardStatsQuery request, CancellationToken cancellationToken)
    {
        var patients = (await _unitOfWork.PatientRepository.GetAllAsync()).ToList();
        var doctors = (await _unitOfWork.DoctorRepository.GetActiveAsync()).ToList();
        var todayAppointments = await _unitOfWork.AppointmentRepository.GetTodayAppointmentsAsync();
        var pendingExams = await _unitOfWork.ExamRepository.GetPendingAsync();
        var lowStock = await _unitOfWork.MedicationRepository.GetLowStockAsync();
        var pendingBills = await _unitOfWork.BillRepository.GetByStatusAsync(BillStatus.Pending);

        var firstDayOfMonth = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
        var paidThisMonth = await _unitOfWork.BillRepository.GetByStatusAsync(BillStatus.Paid);
        var monthRevenue = paidThisMonth
            .Where(b => b.PaidAt >= firstDayOfMonth)
            .Sum(b => b.PatientAmount.Amount);

        _logger.LogInformation("Dashboard stats gerado");

        return new DashboardStatsResponse(
            TotalPatients: patients.Count,
            ActivePatients: patients.Count(p => p.IsActive),
            TotalDoctors: doctors.Count,
            TodayAppointments: todayAppointments.Count(),
            PendingExams: pendingExams.Count(),
            LowStockMedications: lowStock.Count(),
            PendingBills: pendingBills.Count(),
            MonthRevenue: monthRevenue
        );
    }
}
