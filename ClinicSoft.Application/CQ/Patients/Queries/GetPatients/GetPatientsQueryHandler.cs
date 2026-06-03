using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Patients.Common;
using ClinicSoft.Domain.Core.Uow;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.Patients.Queries.GetPatients;

public class GetPatientsQueryHandler : IRequestHandler<GetPatientsQuery, Result<GetPatientsResponse, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetPatientsQueryHandler> _logger;

    public GetPatientsQueryHandler(IUnitOfWork unitOfWork, ILogger<GetPatientsQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<GetPatientsResponse, Success, Error>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
    {
        var all = (await _unitOfWork.PatientRepository.GetActiveAsync()).ToList();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var term = request.SearchTerm.ToLowerInvariant();
            all = all.Where(p =>
                p.Name.FullName.ToLowerInvariant().Contains(term) ||
                p.Email.ToLowerInvariant().Contains(term) ||
                (p.Phone != null && p.Phone.Contains(term)) ||
                (p.Nif != null && p.Nif.Number.Contains(term)) ||
                (p.NumeroUtenteSns != null && p.NumeroUtenteSns.Number.Contains(term))
            ).ToList();
        }

        var total = all.Count;
        var paged = all.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);

        return new GetPatientsResponse
        {
            Patients = paged.Select(p => new PatientResponse
            {
                Id = p.Id, FullName = p.Name.FullName, Email = p.Email,
                Phone = p.Phone, DateOfBirth = p.DateOfBirth, Age = p.Age,
                Gender = p.Gender, BloodType = p.BloodType,
                Nif = p.Nif?.Number, NumeroUtenteSns = p.NumeroUtenteSns?.Number,
                IsActive = p.IsActive, CreatedAt = p.CreatedAt
            }).ToList(),
            TotalCount = total,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalPages = (int)Math.Ceiling((double)total / request.PageSize)
        };
    }
}
