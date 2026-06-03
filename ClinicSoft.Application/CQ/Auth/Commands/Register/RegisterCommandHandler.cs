using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Domain.Core.Interfaces;
using ClinicSoft.Domain.Core.Uow;
using ClinicSoft.Domain.Model;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.Auth.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<RegisterResponse, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<RegisterCommandHandler> _logger;

    public RegisterCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, ILogger<RegisterCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _logger = logger;
    }

    public async Task<Result<RegisterResponse, Success, Error>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.UserRepository.UsernameExistsAsync(request.Username))
            return Error.UsernameAlreadyExists;

        if (await _unitOfWork.UserRepository.EmailExistsAsync(request.Email))
            return Error.EmailAlreadyExists;

        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = _passwordHasher.HashPassword(request.Password),
            Role = request.Role,
            DoctorId = request.DoctorId,
            StaffId = request.StaffId
        };

        var saved = await _unitOfWork.UserRepository.SaveAsync(user);
        await _unitOfWork.CommitAsync(cancellationToken);

        _logger.LogInformation("Utilizador registado: {Username}", saved.Username);

        return new RegisterResponse { Id = saved.Id, Username = saved.Username, Email = saved.Email, Role = saved.Role };
    }
}
