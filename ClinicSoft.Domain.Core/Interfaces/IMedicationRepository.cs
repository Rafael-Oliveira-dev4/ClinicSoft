using ClinicSoft.Domain.Model;

namespace ClinicSoft.Domain.Core.Interfaces;

public interface IMedicationRepository : IRepository<Medication>
{
    Task<IEnumerable<Medication>> GetLowStockAsync();
    Task<IEnumerable<Medication>> SearchByNameAsync(string name);
    Task<Medication?> GetByNameAsync(string name);
    new Task<Medication> SaveAsync(Medication medication);
    new Task<Medication> UpdateAsync(Medication medication);
}
