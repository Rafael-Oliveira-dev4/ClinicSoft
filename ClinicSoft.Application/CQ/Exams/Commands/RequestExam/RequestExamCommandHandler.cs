using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Exams.Common;
using ClinicSoft.Domain.Core.Uow;
using ClinicSoft.Domain.Model;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.Exams.Commands.RequestExam;

public class RequestExamCommandHandler : IRequestHandler<RequestExamCommand, Result<ExamResponse, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RequestExamCommandHandler> _logger;

    public RequestExamCommandHandler(IUnitOfWork unitOfWork, ILogger<RequestExamCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<ExamResponse, Success, Error>> Handle(RequestExamCommand request, CancellationToken cancellationToken)
    {
        var patient = await _unitOfWork.PatientRepository.GetByIdAsync(request.PatientId);
        if (patient == null) return Error.PatientNotFound;

        var exam = new Exam
        {
            PatientId = request.PatientId,
            MedicalRecordId = request.MedicalRecordId,
            Type = request.Type,
            RequestingDoctorNotes = request.RequestingDoctorNotes
        };

        var saved = await _unitOfWork.ExamRepository.SaveAsync(exam);
        await _unitOfWork.CommitAsync(cancellationToken);

        _logger.LogInformation("Exame solicitado: {ExamId}", saved.Id);

        return MapToResponse(saved);
    }

    private static ExamResponse MapToResponse(Exam e) => new()
    {
        Id = e.Id, PatientId = e.PatientId, MedicalRecordId = e.MedicalRecordId,
        Type = e.Type, Status = e.Status, RequestedAt = e.RequestedAt,
        ResultAt = e.ResultAt, ResultNotes = e.ResultNotes, ResultFileUrl = e.ResultFileUrl,
        RequestingDoctorNotes = e.RequestingDoctorNotes, CreatedAt = e.CreatedAt
    };
}
