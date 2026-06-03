using ClinicSoft.Domain.Model;
using ClinicSoft.Domain.Model.Enums;

namespace ClinicSoft.Domain.Core.Interfaces;

public interface IRoomRepository : IRepository<Room>
{
    Task<IEnumerable<Room>> GetByTypeAsync(RoomType type);
    Task<IEnumerable<Room>> GetAvailableAsync();
    new Task<Room> SaveAsync(Room room);
    new Task<Room> UpdateAsync(Room room);
}
