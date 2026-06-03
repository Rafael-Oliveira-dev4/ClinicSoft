using FluentValidation;

namespace ClinicSoft.Application.CQ.Patients.Commands.CreatePatient;

public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
{
    public CreatePatientCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(255);
        RuleFor(x => x.DateOfBirth).LessThan(DateTime.Today).WithMessage("Data de nascimento inválida.");
        RuleFor(x => x.Nif).Length(9).When(x => x.Nif != null);
        RuleFor(x => x.NumeroUtenteSns).Length(9).When(x => x.NumeroUtenteSns != null);
    }
}
