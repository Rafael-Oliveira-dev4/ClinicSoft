using ClinicSoft.Application.Common.Responses;
using ClinicSoft.Domain.Core.Uow;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicSoft.Application.CQ.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResponse, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<LoginCommandHandler> _logger;

    public LoginCommandHandler(IUnitOfWork unitOfWork, ILogger<LoginCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<LoginResponse, Success, Error>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByUsernameAsync(request.Username)
                   ?? await _unitOfWork.UserRepository.GetByEmailAsync(request.Username);

        if (user == null)
        {
            _logger.LogWarning("Login falhado — utilizador não encontrado: {Username}", request.Username);
            return Error.InvalidCredentials;
        }

        if (user.IsDeleted)
        {
            _logger.LogWarning("Login falhado — conta inactiva: {Username}", request.Username);
            return Error.AccountInactive;
        }

        var accessToken = _unitOfWork.TokenService.GenerateToken(user);
        var refreshToken = _unitOfWork.TokenService.GenerateRefreshToken();

        _logger.LogInformation("Login com sucesso: {Username}", request.Username);

        return new LoginResponse
        {
            UserId = user.Id,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddHours(1)
        };
    }
}
