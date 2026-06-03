using FluentValidation;

namespace ClinicSoft.Application.CQ.Appointments.Commands.CreateAppointment;

public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
{
    public CreateAppointmentCommandValidator()
    {
        RuleFor(x => x.PatientId).NotEmpty();
        RuleFor(x => x.DoctorId).NotEmpty();
        RuleFor(x => x.ScheduledAt).GreaterThan(DateTime.UtcNow).WithMessage("A data da consulta deve ser futura.");
        RuleFor(x => x.DurationMinutes).InclusiveBetween(10, 240);
    }
}
