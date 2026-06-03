using ClinicSoft.Domain.Model;
using ClinicSoft.Domain.Model.Enums;

namespace ClinicSoft.Domain.Core.Interfaces;

public interface IDoctorRepository : IRepository<Doctor>
{
    Task<Doctor?> GetByEmailAsync(string email);
    Task<Doctor?> GetByCedulaProfissionalAsync(string cedula);
    Task<IEnumerable<Doctor>> GetBySpecialtyAsync(MedicalSpecialty specialty);
    Task<IEnumerable<Doctor>> SearchByNameAsync(string name);
    Task<IEnumerable<Doctor>> GetActiveAsync();
    Task<bool> EmailExistsAsync(string email, Guid? excludeId = null);
    new Task<Doctor> SaveAsync(Doctor doctor);
    new Task<Doctor> UpdateAsync(Doctor doctor);
}
