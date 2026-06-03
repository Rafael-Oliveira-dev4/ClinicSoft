using ClinicSoft.Domain.Core.Interfaces;
using ClinicSoft.Domain.Model;
using ClinicSoft.Domain.Model.Enums;
using ClinicSoft.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ClinicSoft.Data.Repositories;

public class BillRepository(AppDbContext context) : Repository<Bill>(context), IBillRepository
{
    public async Task<IEnumerable<Bill>> GetByPatientAsync(Guid patientId)
        => await _dbSet
            .Include(b => b.Appointment)
            .Where(b => b.PatientId == patientId)
            .OrderByDescending(b => b.DueDate)
            .ToListAsync();

    public async Task<IEnumerable<Bill>> GetByStatusAsync(BillStatus status)
        => await _dbSet
            .Include(b => b.Patient)
            .Where(b => b.Status == status)
            .OrderBy(b => b.DueDate)
            .ToListAsync();

    public async Task<IEnumerable<Bill>> GetOverdueAsync()
        => await _dbSet
            .Include(b => b.Patient)
            .Where(b => b.Status == BillStatus.Pending && b.DueDate < DateTime.UtcNow)
            .OrderBy(b => b.DueDate)
            .ToListAsync();

    public async Task<Bill?> GetByAppointmentAsync(Guid appointmentId)
        => await _dbSet
            .Include(b => b.Patient)
            .FirstOrDefaultAsync(b => b.AppointmentId == appointmentId);

    public override async Task<Bill> SaveAsync(Bill bill)
    {
        await _dbSet.AddAsync(bill);
        return bill;
    }

    public override Task<Bill> UpdateAsync(Bill bill)
    {
        _dbSet.Update(bill);
        return Task.FromResult(bill);
    }
}
