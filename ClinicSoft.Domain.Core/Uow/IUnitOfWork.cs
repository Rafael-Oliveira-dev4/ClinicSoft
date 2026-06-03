using System.Data.Common;
using ClinicSoft.Domain.Core.Interfaces;

namespace ClinicSoft.Domain.Core.Uow;

public interface IUnitOfWork : IDisposable
{
    IPatientRepository PatientRepository { get; }
    IDoctorRepository DoctorRepository { get; }
    IStaffRepository StaffRepository { get; }
    IAppointmentRepository AppointmentRepository { get; }
    IMedicalRecordRepository MedicalRecordRepository { get; }
    IExamRepository ExamRepository { get; }
    IMedicationRepository MedicationRepository { get; }
    IInsurancePlanRepository InsurancePlanRepository { get; }
    IRoomRepository RoomRepository { get; }
    IProcedureRepository ProcedureRepository { get; }
    IBillRepository BillRepository { get; }
    IUserRepository UserRepository { get; }
    ITokenService TokenService { get; }

    bool Commit();
    Task<bool> CommitAsync(CancellationToken cancellationToken = default);
    Task<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    bool HasChanges();
}
