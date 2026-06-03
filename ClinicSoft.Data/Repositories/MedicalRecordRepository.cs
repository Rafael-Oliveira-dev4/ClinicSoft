using ClinicSoft.Domain.Core.Interfaces;
using ClinicSoft.Domain.Model;
using ClinicSoft.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ClinicSoft.Data.Repositories;

public class MedicalRecordRepository(AppDbContext context) : Repository<MedicalRecord>(context), IMedicalRecordRepository
{
    public async Task<IEnumerable<MedicalRecord>> GetByPatientAsync(Guid patientId)
        => await _dbSet
            .Include(mr => mr.Appointment)
            .Where(mr => mr.PatientId == patientId)
            .OrderByDescending(mr => mr.CreatedAt)
            .ToListAsync();

    public async Task<MedicalRecord?> GetByAppointmentAsync(Guid appointmentId)
        => await _dbSet
            .Include(mr => mr.Prescriptions)
            .Include(mr => mr.Exams)
            .FirstOrDefaultAsync(mr => mr.AppointmentId == appointmentId);

    public async Task<MedicalRecord?> GetWithDetailsAsync(Guid id)
        => await _dbSet
            .Include(mr => mr.Patient)
            .Include(mr => mr.Appointment)
            .Include(mr => mr.Prescriptions)
            .Include(mr => mr.Exams)
            .FirstOrDefaultAsync(mr => mr.Id == id);

    public override async Task<MedicalRecord> SaveAsync(MedicalRecord record)
    {
        await _dbSet.AddAsync(record);
        return record;
    }

    public override Task<MedicalRecord> UpdateAsync(MedicalRecord record)
    {
        _dbSet.Update(record);
        return Task.FromResult(record);
    }
}
