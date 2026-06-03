using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Patients.Common;
using ClinicSoft.Domain.Core.Uow;
using ClinicSoft.Domain.Model;
using ClinicSoft.Domain.Model.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.Patients.Commands.UpdatePatient;

public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, Result<PatientResponse, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdatePatientCommandHandler> _logger;

    public UpdatePatientCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdatePatientCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<PatientResponse, Success, Error>> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = await _unitOfWork.PatientRepository.GetByIdAsync(request.Id);
        if (patient == null) return Error.PatientNotFound;

        if (await _unitOfWork.PatientRepository.EmailExistsAsync(request.Email, request.Id))
            return Error.PatientEmailExists;

        patient.Name = new Name(request.FirstName, request.LastName);
        patient.Email = request.Email;
        patient.Phone = request.Phone;
        patient.DateOfBirth = request.DateOfBirth;
        patient.Gender = request.Gender;
        patient.BloodType = request.BloodType;
        patient.Allergies = request.Allergies;
        patient.ChronicConditions = request.ChronicConditions;
        patient.InsurancePlanId = request.InsurancePlanId;
        patient.UpdateTimestamp();

        await _unitOfWork.PatientRepository.UpdateAsync(patient);
        await _unitOfWork.CommitAsync(cancellationToken);

        _logger.LogInformation("Utente actualizado: {PatientId}", patient.Id);

        return new PatientResponse
        {
            Id = patient.Id, FullName = patient.Name.FullName, Email = patient.Email,
            Phone = patient.Phone, DateOfBirth = patient.DateOfBirth, Age = patient.Age,
            Gender = patient.Gender, BloodType = patient.BloodType,
            Nif = patient.Nif?.Number, NumeroUtenteSns = patient.NumeroUtenteSns?.Number,
            Allergies = patient.Allergies, ChronicConditions = patient.ChronicConditions,
            IsActive = patient.IsActive, CreatedAt = patient.CreatedAt
        };
    }
}
