using ClinicSoft.Application.Common.Responses;
using MediatR;

namespace ClinicSoft.Application.CQ.Patients.Commands.DeletePatient;

public class DeletePatientCommand : IRequest<Result<Success, Error>>
{
    public Guid Id { get; init; }
}
