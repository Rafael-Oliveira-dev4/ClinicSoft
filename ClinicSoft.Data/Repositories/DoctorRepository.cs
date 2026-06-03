using ClinicSoft.Domain.Core.Interfaces;
using ClinicSoft.Domain.Model;
using ClinicSoft.Domain.Model.Enums;
using ClinicSoft.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ClinicSoft.Data.Repositories;

public class DoctorRepository(AppDbContext context) : Repository<Doctor>(context), IDoctorRepository
{
    public async Task<Doctor?> GetByEmailAsync(string email)
        => await _dbSet.FirstOrDefaultAsync(d => d.Email.ToLower() == email.ToLower());

    public async Task<Doctor?> GetByCedulaProfissionalAsync(string cedula)
        => await _dbSet.FirstOrDefaultAsync(d => d.CedulaProfissional.Number == cedula);

    public async Task<IEnumerable<Doctor>> GetBySpecialtyAsync(MedicalSpecialty specialty)
        => await _dbSet.Where(d => d.Specialty == specialty && d.IsActive)
            .OrderBy(d => d.Name.FirstName)
            .ToListAsync();

    public async Task<IEnumerable<Doctor>> SearchByNameAsync(string name)
        => await _dbSet
            .Where(d => d.Name.FirstName.Contains(name) || d.Name.LastName.Contains(name))
            .OrderBy(d => d.Name.FirstName)
            .ToListAsync();

    public async Task<IEnumerable<Doctor>> GetActiveAsync()
        => await _dbSet.Where(d => d.IsActive).OrderBy(d => d.Name.FirstName).ToListAsync();

    public async Task<bool> EmailExistsAsync(string email, Guid? excludeId = null)
        => await _dbSet.AnyAsync(d =>
            d.Email.ToLower() == email.ToLower() &&
            (excludeId == null || d.Id != excludeId));

    public override async Task<Doctor> SaveAsync(Doctor doctor)
    {
        await _dbSet.AddAsync(doctor);
        return doctor;
    }

    public override Task<Doctor> UpdateAsync(Doctor doctor)
    {
        _dbSet.Update(doctor);
        return Task.FromResult(doctor);
    }
}
