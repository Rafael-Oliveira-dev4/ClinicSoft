using ClinicSoft.Domain.Model;
using ClinicSoft.Domain.Model.Enums;

namespace ClinicSoft.Domain.Core.Interfaces;

public interface IBillRepository : IRepository<Bill>
{
    Task<IEnumerable<Bill>> GetByPatientAsync(Guid patientId);
    Task<IEnumerable<Bill>> GetByStatusAsync(BillStatus status);
    Task<IEnumerable<Bill>> GetOverdueAsync();
    Task<Bill?> GetByAppointmentAsync(Guid appointmentId);
    new Task<Bill> SaveAsync(Bill bill);
    new Task<Bill> UpdateAsync(Bill bill);
}
