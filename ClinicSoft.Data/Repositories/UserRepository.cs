using ClinicSoft.Domain.Core.Interfaces;
using ClinicSoft.Domain.Model;
using ClinicSoft.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ClinicSoft.Data.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    private readonly AppDbContext _context = context;
    private readonly DbSet<User> _dbSet = context.Set<User>();

    public async Task<User> SaveAsync(User user)
    {
        await _dbSet.AddAsync(user);
        return user;
    }

    public async Task<User?> GetByIdAsync(Guid id)
        => await _dbSet
            .Include(u => u.Doctor)
            .Include(u => u.Staff)
            .FirstOrDefaultAsync(u => u.Id == id);

    public async Task<IEnumerable<User>> GetAllAsync()
        => await _dbSet.Include(u => u.Doctor).Include(u => u.Staff).ToListAsync();

    public Task<User> UpdateAsync(User user)
    {
        _dbSet.Update(user);
        return Task.FromResult(user);
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await _dbSet.FindAsync(id);
        if (user is not null)
            user.MarkAsDeleted();
    }

    public async Task<IEnumerable<User>> FindAsync(System.Linq.Expressions.Expression<Func<User, bool>> predicate)
        => await _dbSet.Where(predicate).ToListAsync();

    public async Task<User?> FirstOrDefaultAsync(System.Linq.Expressions.Expression<Func<User, bool>> predicate)
        => await _dbSet.FirstOrDefaultAsync(predicate);

    public async Task<bool> ExistsAsync(System.Linq.Expressions.Expression<Func<User, bool>> predicate)
        => await _dbSet.AnyAsync(predicate);

    public async Task<int> CountAsync(System.Linq.Expressions.Expression<Func<User, bool>>? predicate = null)
        => predicate is null ? await _dbSet.CountAsync() : await _dbSet.CountAsync(predicate);

    public async Task<User?> GetByUsernameAsync(string username)
        => await _dbSet
            .Include(u => u.Doctor)
            .Include(u => u.Staff)
            .FirstOrDefaultAsync(u => u.Username == username.ToLower());

    public async Task<User?> GetByEmailAsync(string email)
        => await _dbSet
            .Include(u => u.Doctor)
            .Include(u => u.Staff)
            .FirstOrDefaultAsync(u => u.Email == email.ToLower());

    public async Task<bool> UsernameExistsAsync(string username)
        => await _dbSet.AnyAsync(u => u.Username == username.ToLower());

    public async Task<bool> EmailExistsAsync(string email)
        => await _dbSet.AnyAsync(u => u.Email == email.ToLower());
}
