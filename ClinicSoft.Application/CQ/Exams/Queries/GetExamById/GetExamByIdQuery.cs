using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Exams.Common;
using MediatR;

namespace ClinicSoft.Application.CQ.Exams.Queries.GetExamById;

public class GetExamByIdQuery : IRequest<Result<ExamResponse, Success, Error>>
{
    public Guid Id { get; init; }
}
