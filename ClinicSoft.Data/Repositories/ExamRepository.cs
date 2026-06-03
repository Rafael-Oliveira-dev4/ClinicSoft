using ClinicSoft.Domain.Core.Interfaces;
using ClinicSoft.Domain.Model;
using ClinicSoft.Domain.Model.Enums;
using ClinicSoft.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ClinicSoft.Data.Repositories;

public class ExamRepository(AppDbContext context) : Repository<Exam>(context), IExamRepository
{
    public async Task<IEnumerable<Exam>> GetByPatientAsync(Guid patientId)
        => await _dbSet
            .Where(e => e.PatientId == patientId)
            .OrderByDescending(e => e.RequestedAt)
            .ToListAsync();

    public async Task<IEnumerable<Exam>> GetByStatusAsync(ExamStatus status)
        => await _dbSet
            .Include(e => e.Patient)
            .Where(e => e.Status == status)
            .OrderBy(e => e.RequestedAt)
            .ToListAsync();

    public async Task<IEnumerable<Exam>> GetPendingAsync()
        => await _dbSet
            .Include(e => e.Patient)
            .Where(e => e.Status == ExamStatus.Requested || e.Status == ExamStatus.Collected || e.Status == ExamStatus.Processing)
            .OrderBy(e => e.RequestedAt)
            .ToListAsync();

    public async Task<IEnumerable<Exam>> GetByMedicalRecordAsync(Guid medicalRecordId)
        => await _dbSet
            .Where(e => e.MedicalRecordId == medicalRecordId)
            .OrderByDescending(e => e.RequestedAt)
            .ToListAsync();

    public override async Task<Exam> SaveAsync(Exam exam)
    {
        await _dbSet.AddAsync(exam);
        return exam;
    }

    public override Task<Exam> UpdateAsync(Exam exam)
    {
        _dbSet.Update(exam);
        return Task.FromResult(exam);
    }
}
