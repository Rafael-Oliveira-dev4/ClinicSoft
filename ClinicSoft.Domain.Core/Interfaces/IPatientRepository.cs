using ClinicSoft.Domain.Model;

namespace ClinicSoft.Domain.Core.Interfaces;

public interface IPatientRepository : IRepository<Patient>
{
    Task<Patient?> GetByEmailAsync(string email);
    Task<Patient?> GetByNumeroUtenteSnsAsync(string number);
    Task<Patient?> GetByNifAsync(string nif);
    Task<IEnumerable<Patient>> SearchByNameAsync(string name);
    Task<IEnumerable<Patient>> GetActiveAsync();
    Task<bool> EmailExistsAsync(string email, Guid? excludeId = null);
    new Task<Patient> SaveAsync(Patient patient);
    new Task<Patient> UpdateAsync(Patient patient);
}
