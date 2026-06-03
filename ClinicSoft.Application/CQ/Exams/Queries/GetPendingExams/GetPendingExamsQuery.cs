using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Exams.Common;
using MediatR;

namespace ClinicSoft.Application.CQ.Exams.Queries.GetPendingExams;

public class GetPendingExamsQuery : IRequest<Result<List<ExamResponse>, Success, Error>>;
