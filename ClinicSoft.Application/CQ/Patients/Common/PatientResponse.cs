using ClinicSoft.Domain.Model.Enums;

namespace ClinicSoft.Application.CQ.Patients.Common;

public class PatientResponse
{
    public Guid Id { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int Age { get; set; }
    public Gender Gender { get; set; }
    public BloodType BloodType { get; set; }
    public string? Nif { get; set; }
    public string? NumeroUtenteSns { get; set; }
    public string? Allergies { get; set; }
    public string? ChronicConditions { get; set; }
    public bool IsActive { get; set; }
    public DateTime? CreatedAt { get; set; }
}
