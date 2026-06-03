using ClinicSoft.Application.Common.Responses;
using MediatR;

namespace ClinicSoft.Application.CQ.Auth.Commands.Login;

public class LoginCommand : IRequest<Result<LoginResponse, Success, Error>>
{
    public required string Username { get; init; }
    public required string Password { get; init; }
}

public class LoginResponse
{
    public Guid UserId { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Role { get; set; }
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
    public DateTime ExpiresAt { get; set; }
}
