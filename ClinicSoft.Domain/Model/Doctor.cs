using ClinicSoft.Domain.Common;
using ClinicSoft.Domain.Interfaces;
using ClinicSoft.Domain.Model.Enums;
using ClinicSoft.Domain.Model.ValueObjects;

namespace ClinicSoft.Domain.Model;

public class Doctor : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public required Name Name { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public required CedulaProfissional CedulaProfissional { get; set; }
    public MedicalSpecialty Specialty { get; set; }
    public string? Bio { get; set; }
    public string? ProfileImageUrl { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation
    public List<Appointment> Appointments { get; set; } = [];
}
