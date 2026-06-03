using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Domain.Core.Uow;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.Patients.Commands.DeletePatient;

public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand, Result<Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeletePatientCommandHandler> _logger;

    public DeletePatientCommandHandler(IUnitOfWork unitOfWork, ILogger<DeletePatientCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<Success, Error>> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = await _unitOfWork.PatientRepository.GetByIdAsync(request.Id);
        if (patient == null) return Error.PatientNotFound;

        patient.IsActive = false;
        patient.MarkAsDeleted();
        await _unitOfWork.PatientRepository.UpdateAsync(patient);
        await _unitOfWork.CommitAsync(cancellationToken);

        _logger.LogInformation("Utente desactivado: {PatientId}", request.Id);
        return Success.Ok;
    }
}
