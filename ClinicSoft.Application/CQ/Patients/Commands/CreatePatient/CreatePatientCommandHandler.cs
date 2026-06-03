using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Patients.Common;
using ClinicSoft.Domain.Core.Uow;
using ClinicSoft.Domain.Model;
using ClinicSoft.Domain.Model.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.Patients.Commands.CreatePatient;

public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, Result<PatientResponse, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreatePatientCommandHandler> _logger;

    public CreatePatientCommandHandler(IUnitOfWork unitOfWork, ILogger<CreatePatientCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<PatientResponse, Success, Error>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.PatientRepository.EmailExistsAsync(request.Email))
            return Error.PatientEmailExists;

        var patient = new Patient
        {
            Name = new Name(request.FirstName, request.LastName),
            Email = request.Email,
            Phone = request.Phone,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            BloodType = request.BloodType,
            Nif = request.Nif != null ? new Nif(request.Nif) : null,
            CartaoCidadao = request.CartaoCidadao != null ? new CartaoCidadao(request.CartaoCidadao) : null,
            NumeroUtenteSns = request.NumeroUtenteSns != null ? new NumeroUtenteSns(request.NumeroUtenteSns) : null,
            Allergies = request.Allergies,
            ChronicConditions = request.ChronicConditions,
            InsurancePlanId = request.InsurancePlanId
        };

        var saved = await _unitOfWork.PatientRepository.SaveAsync(patient);
        await _unitOfWork.CommitAsync(cancellationToken);

        _logger.LogInformation("Utente criado: {PatientId}", saved.Id);

        return MapToResponse(saved);
    }

    private static PatientResponse MapToResponse(Patient p) => new()
    {
        Id = p.Id,
        FullName = p.Name.FullName,
        Email = p.Email,
        Phone = p.Phone,
        DateOfBirth = p.DateOfBirth,
        Age = p.Age,
        Gender = p.Gender,
        BloodType = p.BloodType,
        Nif = p.Nif?.Number,
        NumeroUtenteSns = p.NumeroUtenteSns?.Number,
        Allergies = p.Allergies,
        ChronicConditions = p.ChronicConditions,
        IsActive = p.IsActive,
        CreatedAt = p.CreatedAt
    };
}
