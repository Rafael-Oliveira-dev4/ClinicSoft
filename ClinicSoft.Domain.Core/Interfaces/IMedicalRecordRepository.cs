using ClinicSoft.Domain.Model;

namespace ClinicSoft.Domain.Core.Interfaces;

public interface IMedicalRecordRepository : IRepository<MedicalRecord>
{
    Task<IEnumerable<MedicalRecord>> GetByPatientAsync(Guid patientId);
    Task<MedicalRecord?> GetByAppointmentAsync(Guid appointmentId);
    Task<MedicalRecord?> GetWithDetailsAsync(Guid id);
    new Task<MedicalRecord> SaveAsync(MedicalRecord record);
    new Task<MedicalRecord> UpdateAsync(MedicalRecord record);
}
