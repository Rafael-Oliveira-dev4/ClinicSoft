using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Application.CQ.Exams.Common;
using ClinicSoft.Domain.Model.Enums;
using MediatR;

namespace ClinicSoft.Application.CQ.Exams.Commands.RequestExam;

public class RequestExamCommand : IRequest<Result<ExamResponse, Success, Error>>
{
    public Guid PatientId { get; init; }
    public Guid? MedicalRecordId { get; init; }
    public ExamType Type { get; init; }
    public string? RequestingDoctorNotes { get; init; }
}
