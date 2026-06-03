using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Doctors.Common;
using ClinicSoft.Domain.Core.Uow;
using ClinicSoft.Domain.Model;
using ClinicSoft.Domain.Model.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.Doctors.Commands.CreateDoctor;

public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, Result<DoctorResponse, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateDoctorCommandHandler> _logger;

    public CreateDoctorCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateDoctorCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<DoctorResponse, Success, Error>> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.DoctorRepository.EmailExistsAsync(request.Email))
            return Error.DoctorEmailExists;

        var existing = await _unitOfWork.DoctorRepository.GetByCedulaProfissionalAsync(request.CedulaProfissional);
        if (existing != null) return Error.DoctorCedulaExists;

        var doctor = new Doctor
        {
            Name = new Name(request.FirstName, request.LastName),
            Email = request.Email,
            Phone = request.Phone,
            CedulaProfissional = new CedulaProfissional(request.CedulaProfissional),
            Specialty = request.Specialty,
            Bio = request.Bio
        };

        var saved = await _unitOfWork.DoctorRepository.SaveAsync(doctor);
        await _unitOfWork.CommitAsync(cancellationToken);

        _logger.LogInformation("Médico criado: {DoctorId}", saved.Id);

        return new DoctorResponse
        {
            Id = saved.Id, FullName = saved.Name.FullName, Email = saved.Email,
            Phone = saved.Phone, CedulaProfissional = saved.CedulaProfissional.Number,
            Specialty = saved.Specialty, Bio = saved.Bio, IsActive = saved.IsActive, CreatedAt = saved.CreatedAt
        };
    }
}
