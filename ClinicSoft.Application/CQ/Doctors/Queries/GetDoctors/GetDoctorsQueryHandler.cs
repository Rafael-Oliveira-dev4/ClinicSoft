using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Doctors.Common;
using ClinicSoft.Domain.Core.Uow;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.Doctors.Queries.GetDoctors;

public class GetDoctorsQueryHandler : IRequestHandler<GetDoctorsQuery, Result<List<DoctorResponse>, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetDoctorsQueryHandler> _logger;

    public GetDoctorsQueryHandler(IUnitOfWork unitOfWork, ILogger<GetDoctorsQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<List<DoctorResponse>, Success, Error>> Handle(GetDoctorsQuery request, CancellationToken cancellationToken)
    {
        var doctors = await _unitOfWork.DoctorRepository.GetActiveAsync();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var term = request.SearchTerm.ToLowerInvariant();
            doctors = doctors.Where(d => d.Name.FullName.ToLowerInvariant().Contains(term) ||
                                         d.Email.ToLowerInvariant().Contains(term));
        }

        return doctors.Select(d => new DoctorResponse
        {
            Id = d.Id, FullName = d.Name.FullName, Email = d.Email, Phone = d.Phone,
            CedulaProfissional = d.CedulaProfissional.Number, Specialty = d.Specialty,
            Bio = d.Bio, IsActive = d.IsActive, CreatedAt = d.CreatedAt
        }).ToList();
    }
}
