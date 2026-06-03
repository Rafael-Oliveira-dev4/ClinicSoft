using ClinicSoft.Domain.Model;
using ClinicSoft.Domain.Model.Enums;

namespace ClinicSoft.Domain.Core.Interfaces;

public interface IAppointmentRepository : IRepository<Appointment>
{
    Task<IEnumerable<Appointment>> GetByPatientAsync(Guid patientId);
    Task<IEnumerable<Appointment>> GetByDoctorAsync(Guid doctorId);
    Task<IEnumerable<Appointment>> GetByDoctorAndDateAsync(Guid doctorId, DateOnly date);
    Task<IEnumerable<Appointment>> GetByStatusAsync(AppointmentStatus status);
    Task<IEnumerable<Appointment>> GetTodayAppointmentsAsync();
    Task<IEnumerable<Appointment>> GetByDateRangeAsync(DateTime from, DateTime to);
    new Task<Appointment> SaveAsync(Appointment appointment);
    new Task<Appointment> UpdateAsync(Appointment appointment);
}
