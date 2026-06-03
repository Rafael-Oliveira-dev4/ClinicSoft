using ClinicSoft.Application.Common.Responses;
using MediatR;

namespace ClinicSoft.Application.CQ.Auth.Commands.Register;

public class RegisterCommand : IRequest<Result<RegisterResponse, Success, Error>>
{
    public required string Username { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
    public string Role { get; init; } = "Staff";
    public Guid? DoctorId { get; init; }
    public Guid? StaffId { get; init; }
}

public class RegisterResponse
{
    public Guid Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Role { get; set; }
}
