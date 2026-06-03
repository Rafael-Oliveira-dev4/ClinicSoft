using System.Security.Claims;
using ClinicSoft.Domain.Model;

namespace ClinicSoft.Domain.Core.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}
