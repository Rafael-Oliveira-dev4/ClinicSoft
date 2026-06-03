using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Exams.Common;
using ClinicSoft.Domain.Core.Uow;
using ClinicSoft.Domain.Model.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.Exams.Commands.UpdateExamResult;

public class UpdateExamResultCommandHandler : IRequestHandler<UpdateExamResultCommand, Result<ExamResponse, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateExamResultCommandHandler> _logger;

    public UpdateExamResultCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateExamResultCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<ExamResponse, Success, Error>> Handle(UpdateExamResultCommand request, CancellationToken cancellationToken)
    {
        var exam = await _unitOfWork.ExamRepository.GetByIdAsync(request.Id);
        if (exam == null) return Error.ExamNotFound;
        if (exam.Status == ExamStatus.ResultAvailable) return Error.ExamAlreadyCompleted;

        exam.MarkResultAvailable(request.ResultNotes, request.ResultFileUrl);
        await _unitOfWork.ExamRepository.UpdateAsync(exam);
        await _unitOfWork.CommitAsync(cancellationToken);

        _logger.LogInformation("Resultado de exame disponível: {ExamId}", exam.Id);

        return new ExamResponse
        {
            Id = exam.Id, PatientId = exam.PatientId, MedicalRecordId = exam.MedicalRecordId,
            Type = exam.Type, Status = exam.Status, RequestedAt = exam.RequestedAt,
            ResultAt = exam.ResultAt, ResultNotes = exam.ResultNotes,
            ResultFileUrl = exam.ResultFileUrl, CreatedAt = exam.CreatedAt
        };
    }
}
