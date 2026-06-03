using ClinicSoft.Domain.Core.Interfaces;
using ClinicSoft.Domain.Model;
using ClinicSoft.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ClinicSoft.Data.Repositories;

public class PatientRepository(AppDbContext context) : Repository<Patient>(context), IPatientRepository
{
    public async Task<Patient?> GetByEmailAsync(string email)
        => await _dbSet.FirstOrDefaultAsync(p => p.Email.ToLower() == email.ToLower());

    public async Task<Patient?> GetByNifAsync(string nif)
        => await _dbSet.FirstOrDefaultAsync(p => p.Nif != null && p.Nif.Number == nif);

    public async Task<Patient?> GetByNumeroUtenteSnsAsync(string number)
        => await _dbSet.FirstOrDefaultAsync(p => p.NumeroUtenteSns != null && p.NumeroUtenteSns.Number == number);

    public async Task<IEnumerable<Patient>> SearchByNameAsync(string name)
        => await _dbSet
            .Where(p => p.Name.FirstName.Contains(name) || p.Name.LastName.Contains(name))
            .OrderBy(p => p.Name.FirstName)
            .ToListAsync();

    public async Task<IEnumerable<Patient>> GetActiveAsync()
        => await _dbSet.Where(p => p.IsActive).OrderBy(p => p.Name.FirstName).ToListAsync();

    public async Task<bool> EmailExistsAsync(string email, Guid? excludeId = null)
        => await _dbSet.AnyAsync(p =>
            p.Email.ToLower() == email.ToLower() &&
            (excludeId == null || p.Id != excludeId));

    public override async Task<Patient> SaveAsync(Patient patient)
    {
        await _dbSet.AddAsync(patient);
        return patient;
    }

    public override Task<Patient> UpdateAsync(Patient patient)
    {
        _dbSet.Update(patient);
        return Task.FromResult(patient);
    }
}
