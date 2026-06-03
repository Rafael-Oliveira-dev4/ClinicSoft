using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Doctors.Common;
using ClinicSoft.Domain.Core.Uow;
using ClinicSoft.Domain.Model.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.Doctors.Commands.UpdateDoctor;

public class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand, Result<DoctorResponse, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateDoctorCommandHandler> _logger;

    public UpdateDoctorCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateDoctorCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<DoctorResponse, Success, Error>> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
    {
        var doctor = await _unitOfWork.DoctorRepository.GetByIdAsync(request.Id);
        if (doctor == null) return Error.DoctorNotFound;

        if (await _unitOfWork.DoctorRepository.EmailExistsAsync(request.Email, request.Id))
            return Error.DoctorEmailExists;

        doctor.Name = new Name(request.FirstName, request.LastName);
        doctor.Email = request.Email;
        doctor.Phone = request.Phone;
        doctor.Specialty = request.Specialty;
        doctor.Bio = request.Bio;
        doctor.UpdateTimestamp();

        await _unitOfWork.DoctorRepository.UpdateAsync(doctor);
        await _unitOfWork.CommitAsync(cancellationToken);

        _logger.LogInformation("Médico actualizado: {DoctorId}", doctor.Id);

        return new DoctorResponse
        {
            Id = doctor.Id, FullName = doctor.Name.FullName, Email = doctor.Email,
            Phone = doctor.Phone, CedulaProfissional = doctor.CedulaProfissional.Number,
            Specialty = doctor.Specialty, Bio = doctor.Bio, IsActive = doctor.IsActive, CreatedAt = doctor.CreatedAt
        };
    }
}
