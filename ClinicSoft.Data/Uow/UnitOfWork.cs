using ClinicSoft.Domain.Core.Interfaces;
using ClinicSoft.Domain.Core.Uow;
using ClinicSoft.Data.Context;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;

namespace ClinicSoft.Data.Uow;

internal class UnitOfWork(
    AppDbContext context,
    IPatientRepository patientRepository,
    IDoctorRepository doctorRepository,
    IStaffRepository staffRepository,
    IAppointmentRepository appointmentRepository,
    IMedicalRecordRepository medicalRecordRepository,
    IExamRepository examRepository,
    IMedicationRepository medicationRepository,
    IInsurancePlanRepository insurancePlanRepository,
    IRoomRepository roomRepository,
    IProcedureRepository procedureRepository,
    IBillRepository billRepository,
    IUserRepository userRepository,
    ITokenService tokenService
) : IUnitOfWork
{
    private IDbContextTransaction? _transaction;
    private bool _disposed;

    public IPatientRepository PatientRepository => patientRepository;
    public IDoctorRepository DoctorRepository => doctorRepository;
    public IStaffRepository StaffRepository => staffRepository;
    public IAppointmentRepository AppointmentRepository => appointmentRepository;
    public IMedicalRecordRepository MedicalRecordRepository => medicalRecordRepository;
    public IExamRepository ExamRepository => examRepository;
    public IMedicationRepository MedicationRepository => medicationRepository;
    public IInsurancePlanRepository InsurancePlanRepository => insurancePlanRepository;
    public IRoomRepository RoomRepository => roomRepository;
    public IProcedureRepository ProcedureRepository => procedureRepository;
    public IBillRepository BillRepository => billRepository;
    public IUserRepository UserRepository => userRepository;
    public ITokenService TokenService => tokenService;

    public bool Commit() => context.SaveChanges() > 0;

    public async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
        => await context.SaveChangesAsync(cancellationToken) > 0;

    public async Task<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        return _transaction.GetDbTransaction();
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await context.SaveChangesAsync(cancellationToken);
            if (_transaction is not null)
                await _transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            if (_transaction is not null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is not null)
            await _transaction.RollbackAsync(cancellationToken);
    }

    public bool HasChanges() => context.ChangeTracker.HasChanges();

    public void Dispose()
    {
        if (_disposed) return;
        _transaction?.Dispose();
        context.Dispose();
        _disposed = true;
        GC.SuppressFinalize(this);
    }
}
