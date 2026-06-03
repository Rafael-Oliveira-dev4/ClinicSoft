using ClinicSoft.Domain.Core.Interfaces;
using ClinicSoft.Domain.Model;
using ClinicSoft.Domain.Model.Enums;
using ClinicSoft.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ClinicSoft.Data.Repositories;

public class ProcedureRepository(AppDbContext context) : Repository<Procedure>(context), IProcedureRepository
{
    public async Task<IEnumerable<Procedure>> GetBySpecialtyAsync(MedicalSpecialty specialty)
        => await _dbSet.Where(p => p.Category == specialty && p.IsActive)
            .OrderBy(p => p.Name)
            .ToListAsync();

    public async Task<IEnumerable<Procedure>> GetActiveAsync()
        => await _dbSet.Where(p => p.IsActive).OrderBy(p => p.Name).ToListAsync();

    public async Task<IEnumerable<Procedure>> SearchByNameAsync(string name)
        => await _dbSet.Where(p => p.Name.Contains(name))
            .OrderBy(p => p.Name)
            .ToListAsync();

    public async Task<Procedure?> GetByCodeAsync(string code)
        => await _dbSet.FirstOrDefaultAsync(p => p.Code == code);

    public override async Task<Procedure> SaveAsync(Procedure procedure)
    {
        await _dbSet.AddAsync(procedure);
        return procedure;
    }

    public override Task<Procedure> UpdateAsync(Procedure procedure)
    {
        _dbSet.Update(procedure);
        return Task.FromResult(procedure);
    }
}
