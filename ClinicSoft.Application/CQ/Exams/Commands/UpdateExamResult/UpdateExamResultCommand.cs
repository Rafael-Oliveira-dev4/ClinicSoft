using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Exams.Common;
using MediatR;

namespace ClinicSoft.Application.CQ.Exams.Commands.UpdateExamResult;

public class UpdateExamResultCommand : IRequest<Result<ExamResponse, Success, Error>>
{
    public Guid Id { get; init; }
    public required string ResultNotes { get; init; }
    public string? ResultFileUrl { get; init; }
}
