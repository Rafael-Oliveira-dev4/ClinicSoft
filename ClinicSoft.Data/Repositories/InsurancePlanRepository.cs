using ClinicSoft.Domain.Core.Interfaces;
using ClinicSoft.Domain.Model;
using ClinicSoft.Domain.Model.Enums;
using ClinicSoft.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ClinicSoft.Data.Repositories;

public class InsurancePlanRepository(AppDbContext context) : Repository<InsurancePlan>(context), IInsurancePlanRepository
{
    public async Task<IEnumerable<InsurancePlan>> GetActiveAsync()
        => await _dbSet.Where(ip => ip.IsActive).OrderBy(ip => ip.Name).ToListAsync();

    public async Task<IEnumerable<InsurancePlan>> GetByTypeAsync(InsuranceType type)
        => await _dbSet.Where(ip => ip.Type == type && ip.IsActive).OrderBy(ip => ip.Name).ToListAsync();

    public async Task<InsurancePlan?> GetByAnsCodeAsync(string ansCode)
        => await _dbSet.FirstOrDefaultAsync(ip => ip.AnsCode == ansCode);

    public override async Task<InsurancePlan> SaveAsync(InsurancePlan plan)
    {
        await _dbSet.AddAsync(plan);
        return plan;
    }

    public override Task<InsurancePlan> UpdateAsync(InsurancePlan plan)
    {
        _dbSet.Update(plan);
        return Task.FromResult(plan);
    }
}
