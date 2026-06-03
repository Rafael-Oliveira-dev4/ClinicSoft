using ClinicSoft.Domain.Core.Interfaces;
using ClinicSoft.Domain.Model;
using ClinicSoft.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ClinicSoft.Data.Repositories;

public class MedicationRepository(AppDbContext context) : Repository<Medication>(context), IMedicationRepository
{
    public async Task<IEnumerable<Medication>> GetLowStockAsync()
        => await _dbSet
            .Where(m => m.CurrentStock <= m.MinimumStock)
            .OrderBy(m => m.CurrentStock)
            .ToListAsync();

    public async Task<IEnumerable<Medication>> SearchByNameAsync(string name)
        => await _dbSet
            .Where(m => m.Name.Contains(name) || m.ActiveSubstance.Contains(name))
            .OrderBy(m => m.Name)
            .ToListAsync();

    public async Task<Medication?> GetByNameAsync(string name)
        => await _dbSet.FirstOrDefaultAsync(m => m.Name.ToLower() == name.ToLower());

    public override async Task<Medication> SaveAsync(Medication medication)
    {
        await _dbSet.AddAsync(medication);
        return medication;
    }

    public override Task<Medication> UpdateAsync(Medication medication)
    {
        _dbSet.Update(medication);
        return Task.FromResult(medication);
    }
}
