using ClinicSoft.Domain.Model;
using ClinicSoft.Domain.Model.Enums;

namespace ClinicSoft.Domain.Core.Interfaces;

public interface IProcedureRepository : IRepository<Procedure>
{
    Task<IEnumerable<Procedure>> GetBySpecialtyAsync(MedicalSpecialty specialty);
    Task<IEnumerable<Procedure>> GetActiveAsync();
    Task<IEnumerable<Procedure>> SearchByNameAsync(string name);
    Task<Procedure?> GetByCodeAsync(string code);
    new Task<Procedure> SaveAsync(Procedure procedure);
    new Task<Procedure> UpdateAsync(Procedure procedure);
}
