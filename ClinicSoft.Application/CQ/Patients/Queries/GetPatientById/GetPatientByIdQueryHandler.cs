using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Patients.Common;
using ClinicSoft.Domain.Core.Uow;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.Patients.Queries.GetPatientById;

public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, Result<PatientResponse, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetPatientByIdQueryHandler> _logger;

    public GetPatientByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetPatientByIdQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<PatientResponse, Success, Error>> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
    {
        var patient = await _unitOfWork.PatientRepository.GetByIdAsync(request.Id);
        if (patient == null) return Error.PatientNotFound;

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
