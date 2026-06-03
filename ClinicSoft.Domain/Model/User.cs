using ClinicSoft.Domain.Common;
using ClinicSoft.Domain.Interfaces;

namespace ClinicSoft.Domain.Model;

public class User : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public string Role { get; set; } = "Staff";
    public Guid? DoctorId { get; set; }
    public Guid? StaffId { get; set; }
    public string? ProfileImageUrl { get; set; }

    // Navigation
    public Doctor? Doctor { get; set; }
    public Staff? Staff { get; set; }
}
