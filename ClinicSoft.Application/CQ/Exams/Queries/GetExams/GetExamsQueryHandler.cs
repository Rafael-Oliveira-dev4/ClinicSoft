using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Exams.Common;
using ClinicSoft.Domain.Core.Uow;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.Exams.Queries.GetExams;

public class GetExamsQueryHandler : IRequestHandler<GetExamsQuery, Result<List<ExamResponse>, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetExamsQueryHandler> _logger;

    public GetExamsQueryHandler(IUnitOfWork unitOfWork, ILogger<GetExamsQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<List<ExamResponse>, Success, Error>> Handle(GetExamsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Domain.Model.Exam> exams;

        if (request.PatientId.HasValue)
            exams = await _unitOfWork.ExamRepository.GetByPatientAsync(request.PatientId.Value);
        else if (request.Status.HasValue)
            exams = await _unitOfWork.ExamRepository.GetByStatusAsync(request.Status.Value);
        else
            exams = await _unitOfWork.ExamRepository.GetAllAsync();

        return exams.Select(e => new ExamResponse
        {
            Id = e.Id, PatientId = e.PatientId, MedicalRecordId = e.MedicalRecordId,
            Type = e.Type, Status = e.Status, RequestedAt = e.RequestedAt,
            ResultAt = e.ResultAt, ResultNotes = e.ResultNotes,
            ResultFileUrl = e.ResultFileUrl, CreatedAt = e.CreatedAt
        }).ToList();
    }
}
