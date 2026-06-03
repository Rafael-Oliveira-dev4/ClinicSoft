using ClinicSoft.Domain.Model;
using ClinicSoft.Domain.Model.Enums;

namespace ClinicSoft.Domain.Core.Interfaces;

public interface IExamRepository : IRepository<Exam>
{
    Task<IEnumerable<Exam>> GetByPatientAsync(Guid patientId);
    Task<IEnumerable<Exam>> GetByStatusAsync(ExamStatus status);
    Task<IEnumerable<Exam>> GetPendingAsync();
    Task<IEnumerable<Exam>> GetByMedicalRecordAsync(Guid medicalRecordId);
    new Task<Exam> SaveAsync(Exam exam);
    new Task<Exam> UpdateAsync(Exam exam);
}
