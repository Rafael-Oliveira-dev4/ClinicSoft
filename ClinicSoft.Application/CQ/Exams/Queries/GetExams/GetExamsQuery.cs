using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Exams.Common;
using ClinicSoft.Domain.Model.Enums;
using MediatR;

namespace ClinicSoft.Application.CQ.Exams.Queries.GetExams;

public class GetExamsQuery : IRequest<Result<List<ExamResponse>, Success, Error>>
{
    public Guid? PatientId { get; init; }
    public ExamStatus? Status { get; init; }
}
