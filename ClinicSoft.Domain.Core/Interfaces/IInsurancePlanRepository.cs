using ClinicSoft.Domain.Model;
using ClinicSoft.Domain.Model.Enums;

namespace ClinicSoft.Domain.Core.Interfaces;

public interface IInsurancePlanRepository : IRepository<InsurancePlan>
{
    Task<IEnumerable<InsurancePlan>> GetActiveAsync();
    Task<IEnumerable<InsurancePlan>> GetByTypeAsync(InsuranceType type);
    Task<InsurancePlan?> GetByAnsCodeAsync(string ansCode);
    new Task<InsurancePlan> SaveAsync(InsurancePlan plan);
    new Task<InsurancePlan> UpdateAsync(InsurancePlan plan);
}
