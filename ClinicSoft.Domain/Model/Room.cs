using ClinicSoft.Domain.Common;
using ClinicSoft.Domain.Interfaces;
using ClinicSoft.Domain.Model.Enums;

namespace ClinicSoft.Domain.Model;

public class Room : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public RoomType Type { get; set; }
    public int Floor { get; set; }
    public bool IsAvailable { get; set; } = true;

    // Navigation
    public List<Appointment> Appointments { get; set; } = [];
}
