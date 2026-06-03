using ClinicSoft.Domain.Model.Enums;

namespace ClinicSoft.Application.CQ.Doctors.Common;

public class DoctorResponse
{
    public Guid Id { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public required string CedulaProfissional { get; set; }
    public MedicalSpecialty Specialty { get; set; }
    public string? Bio { get; set; }
    public bool IsActive { get; set; }
    public DateTime? CreatedAt { get; set; }
}
