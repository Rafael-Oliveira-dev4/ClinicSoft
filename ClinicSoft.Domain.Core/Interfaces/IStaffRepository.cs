using ClinicSoft.Domain.Model;
using ClinicSoft.Domain.Model.Enums;

namespace ClinicSoft.Domain.Core.Interfaces;

public interface IStaffRepository : IRepository<Staff>
{
    Task<Staff?> GetByEmailAsync(string email);
    Task<IEnumerable<Staff>> GetByRoleAsync(StaffRole role);
    Task<IEnumerable<Staff>> SearchByNameAsync(string name);
    Task<IEnumerable<Staff>> GetActiveAsync();
    Task<bool> EmailExistsAsync(string email, Guid? excludeId = null);
    new Task<Staff> SaveAsync(Staff staff);
    new Task<Staff> UpdateAsync(Staff staff);
}
