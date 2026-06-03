using FluentValidation;

namespace ClinicSoft.Application.CQ.Auth.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username ou email é obrigatório.");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password é obrigatória.");
    }
}
