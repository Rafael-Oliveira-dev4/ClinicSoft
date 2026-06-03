using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Doctors.Common;
using ClinicSoft.Domain.Core.Uow;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.Doctors.Queries.GetDoctorById;

public class GetDoctorByIdQueryHandler : IRequestHandler<GetDoctorByIdQuery, Result<DoctorResponse, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetDoctorByIdQueryHandler> _logger;

    public GetDoctorByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetDoctorByIdQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<DoctorResponse, Success, Error>> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
    {
        var doctor = await _unitOfWork.DoctorRepository.GetByIdAsync(request.Id);
        if (doctor == null) return Error.DoctorNotFound;

        return new DoctorResponse
        {
            Id = doctor.Id, FullName = doctor.Name.FullName, Email = doctor.Email, Phone = doctor.Phone,
            CedulaProfissional = doctor.CedulaProfissional.Number, Specialty = doctor.Specialty,
            Bio = doctor.Bio, IsActive = doctor.IsActive, CreatedAt = doctor.CreatedAt
        };
    }
}
