using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Doctors.Common;
using ClinicSoft.Domain.Core.Uow;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.Doctors.Queries.GetDoctorsBySpecialty;

public class GetDoctorsBySpecialtyQueryHandler : IRequestHandler<GetDoctorsBySpecialtyQuery, Result<List<DoctorResponse>, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetDoctorsBySpecialtyQueryHandler> _logger;

    public GetDoctorsBySpecialtyQueryHandler(IUnitOfWork unitOfWork, ILogger<GetDoctorsBySpecialtyQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<List<DoctorResponse>, Success, Error>> Handle(GetDoctorsBySpecialtyQuery request, CancellationToken cancellationToken)
    {
        var doctors = await _unitOfWork.DoctorRepository.GetBySpecialtyAsync(request.Specialty);

        return doctors.Select(d => new DoctorResponse
        {
            Id = d.Id, FullName = d.Name.FullName, Email = d.Email, Phone = d.Phone,
            CedulaProfissional = d.CedulaProfissional.Number, Specialty = d.Specialty,
            Bio = d.Bio, IsActive = d.IsActive, CreatedAt = d.CreatedAt
        }).ToList();
    }
}
