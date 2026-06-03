using ClinicSoft.Domain.Core.Interfaces;
using ClinicSoft.Domain.Model;
using ClinicSoft.Domain.Model.Enums;
using ClinicSoft.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ClinicSoft.Data.Repositories;

public class StaffRepository(AppDbContext context) : Repository<Staff>(context), IStaffRepository
{
    public async Task<Staff?> GetByEmailAsync(string email)
        => await _dbSet.FirstOrDefaultAsync(s => s.Email.ToLower() == email.ToLower());

    public async Task<IEnumerable<Staff>> GetByRoleAsync(StaffRole role)
        => await _dbSet.Where(s => s.Role == role && s.IsActive)
            .OrderBy(s => s.Name.FirstName)
            .ToListAsync();

    public async Task<IEnumerable<Staff>> SearchByNameAsync(string name)
        => await _dbSet
            .Where(s => s.Name.FirstName.Contains(name) || s.Name.LastName.Contains(name))
            .OrderBy(s => s.Name.FirstName)
            .ToListAsync();

    public async Task<IEnumerable<Staff>> GetActiveAsync()
        => await _dbSet.Where(s => s.IsActive).OrderBy(s => s.Name.FirstName).ToListAsync();

    public async Task<bool> EmailExistsAsync(string email, Guid? excludeId = null)
        => await _dbSet.AnyAsync(s =>
            s.Email.ToLower() == email.ToLower() &&
            (excludeId == null || s.Id != excludeId));

    public override async Task<Staff> SaveAsync(Staff staff)
    {
        await _dbSet.AddAsync(staff);
        return staff;
    }

    public override Task<Staff> UpdateAsync(Staff staff)
    {
        _dbSet.Update(staff);
        return Task.FromResult(staff);
    }
}
