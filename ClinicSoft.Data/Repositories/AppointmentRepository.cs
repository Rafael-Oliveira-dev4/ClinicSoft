using ClinicSoft.Domain.Core.Interfaces;
using ClinicSoft.Domain.Model;
using ClinicSoft.Domain.Model.Enums;
using ClinicSoft.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ClinicSoft.Data.Repositories;

public class AppointmentRepository(AppDbContext context) : Repository<Appointment>(context), IAppointmentRepository
{
    public async Task<IEnumerable<Appointment>> GetByPatientAsync(Guid patientId)
        => await _dbSet
            .Include(a => a.Doctor)
            .Include(a => a.Room)
            .Where(a => a.PatientId == patientId)
            .OrderByDescending(a => a.ScheduledAt)
            .ToListAsync();

    public async Task<IEnumerable<Appointment>> GetByDoctorAsync(Guid doctorId)
        => await _dbSet
            .Include(a => a.Patient)
            .Include(a => a.Room)
            .Where(a => a.DoctorId == doctorId)
            .OrderByDescending(a => a.ScheduledAt)
            .ToListAsync();

    public async Task<IEnumerable<Appointment>> GetByDoctorAndDateAsync(Guid doctorId, DateOnly date)
        => await _dbSet
            .Include(a => a.Patient)
            .Include(a => a.Room)
            .Where(a => a.DoctorId == doctorId &&
                        DateOnly.FromDateTime(a.ScheduledAt) == date)
            .OrderBy(a => a.ScheduledAt)
            .ToListAsync();

    public async Task<IEnumerable<Appointment>> GetByStatusAsync(AppointmentStatus status)
        => await _dbSet
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Where(a => a.Status == status)
            .OrderBy(a => a.ScheduledAt)
            .ToListAsync();

    public async Task<IEnumerable<Appointment>> GetTodayAppointmentsAsync()
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        return await _dbSet
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Include(a => a.Room)
            .Where(a => DateOnly.FromDateTime(a.ScheduledAt) == today)
            .OrderBy(a => a.ScheduledAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Appointment>> GetByDateRangeAsync(DateTime from, DateTime to)
        => await _dbSet
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Where(a => a.ScheduledAt >= from && a.ScheduledAt <= to)
            .OrderBy(a => a.ScheduledAt)
            .ToListAsync();

    public override async Task<Appointment> SaveAsync(Appointment appointment)
    {
        await _dbSet.AddAsync(appointment);
        return appointment;
    }

    public override Task<Appointment> UpdateAsync(Appointment appointment)
    {
        _dbSet.Update(appointment);
        return Task.FromResult(appointment);
    }
}
