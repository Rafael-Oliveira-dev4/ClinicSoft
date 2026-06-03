using ClinicSoft.Application.Common.Responses;
using MediatR;

namespace ClinicSoft.Application.CQ.Dashboard.Queries.GetDashboardStats;

public class GetDashboardStatsQuery : IRequest<Result<DashboardStatsResponse, Success, Error>>;

public record DashboardStatsResponse(
    int TotalPatients,
    int ActivePatients,
    int TotalDoctors,
    int TodayAppointments,
    int PendingExams,
    int LowStockMedications,
    int PendingBills,
    decimal MonthRevenue
);
