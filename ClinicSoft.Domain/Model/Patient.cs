using ClinicSoft.Domain.Common;
using ClinicSoft.Domain.Interfaces;
using ClinicSoft.Domain.Model.Enums;
using ClinicSoft.Domain.Model.ValueObjects;

namespace ClinicSoft.Domain.Model;

public class Patient : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public required Name Name { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public BloodType BloodType { get; set; }
    public Nif? Nif { get; set; }
    public CartaoCidadao? CartaoCidadao { get; set; }
    public NumeroUtenteSns? NumeroUtenteSns { get; set; }
    public Address? Address { get; set; }
    public string? Allergies { get; set; }
    public string? ChronicConditions { get; set; }
    public Guid? InsurancePlanId { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation
    public InsurancePlan? InsurancePlan { get; set; }
    public List<Appointment> Appointments { get; set; } = [];
    public List<MedicalRecord> MedicalRecords { get; set; } = [];
    public List<Exam> Exams { get; set; } = [];
    public List<Bill> Bills { get; set; } = [];

    public int Age => DateTime.Today.Year - DateOfBirth.Year -
        (DateTime.Today.DayOfYear < DateOfBirth.DayOfYear ? 1 : 0);
}
