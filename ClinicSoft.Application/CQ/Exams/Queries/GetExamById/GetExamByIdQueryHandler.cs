using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Exams.Common;
using ClinicSoft.Domain.Core.Uow;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.Exams.Queries.GetExamById;

public class GetExamByIdQueryHandler : IRequestHandler<GetExamByIdQuery, Result<ExamResponse, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetExamByIdQueryHandler> _logger;

    public GetExamByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetExamByIdQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<ExamResponse, Success, Error>> Handle(GetExamByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await _unitOfWork.ExamRepository.GetByIdAsync(request.Id);
        if (e == null) return Error.ExamNotFound;

        return new ExamResponse
        {
            Id = e.Id, PatientId = e.PatientId, MedicalRecordId = e.MedicalRecordId,
            Type = e.Type, Status = e.Status, RequestedAt = e.RequestedAt,
            ResultAt = e.ResultAt, ResultNotes = e.ResultNotes,
            ResultFileUrl = e.ResultFileUrl, CreatedAt = e.CreatedAt
        };
    }
}
