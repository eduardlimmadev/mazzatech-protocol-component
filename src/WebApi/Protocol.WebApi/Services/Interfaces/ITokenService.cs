using System.Security.Claims;

namespace Protocol.WebApi.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken();
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
