using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Exams.Common;
using ClinicSoft.Domain.Core.Uow;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.Exams.Queries.GetPendingExams;

public class GetPendingExamsQueryHandler : IRequestHandler<GetPendingExamsQuery, Result<List<ExamResponse>, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetPendingExamsQueryHandler> _logger;

    public GetPendingExamsQueryHandler(IUnitOfWork unitOfWork, ILogger<GetPendingExamsQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<List<ExamResponse>, Success, Error>> Handle(GetPendingExamsQuery request, CancellationToken cancellationToken)
    {
        var exams = await _unitOfWork.ExamRepository.GetPendingAsync();

        return exams.Select(e => new ExamResponse
        {
            Id = e.Id, PatientId = e.PatientId, MedicalRecordId = e.MedicalRecordId,
            Type = e.Type, Status = e.Status, RequestedAt = e.RequestedAt,
            RequestingDoctorNotes = e.RequestingDoctorNotes, CreatedAt = e.CreatedAt
        }).ToList();
    }
}
