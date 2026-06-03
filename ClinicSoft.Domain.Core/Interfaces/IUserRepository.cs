using ClinicSoft.Domain.Model;

namespace ClinicSoft.Domain.Core.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByEmailAsync(string email);
    Task<bool> UsernameExistsAsync(string username);
    Task<bool> EmailExistsAsync(string email);
    new Task<User> SaveAsync(User user);
    new Task<User> UpdateAsync(User user);
}
