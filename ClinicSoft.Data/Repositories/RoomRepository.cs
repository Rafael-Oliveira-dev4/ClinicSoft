using ClinicSoft.Domain.Core.Interfaces;
using ClinicSoft.Domain.Model;
using ClinicSoft.Domain.Model.Enums;
using ClinicSoft.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ClinicSoft.Data.Repositories;

public class RoomRepository(AppDbContext context) : Repository<Room>(context), IRoomRepository
{
    public async Task<IEnumerable<Room>> GetByTypeAsync(RoomType type)
        => await _dbSet.Where(r => r.Type == type && !r.IsDeleted).OrderBy(r => r.Name).ToListAsync();

    public async Task<IEnumerable<Room>> GetAvailableAsync()
        => await _dbSet.Where(r => r.IsAvailable).OrderBy(r => r.Name).ToListAsync();

    public override async Task<Room> SaveAsync(Room room)
    {
        await _dbSet.AddAsync(room);
        return room;
    }

    public override Task<Room> UpdateAsync(Room room)
    {
        _dbSet.Update(room);
        return Task.FromResult(room);
    }
}
