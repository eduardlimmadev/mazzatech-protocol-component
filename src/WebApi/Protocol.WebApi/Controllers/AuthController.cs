using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Protocol.WebApi.RequestModels;
using Protocol.WebApi.Services;
using Protocol.WebApi.Services.Interfaces;

namespace Protocol.WebApi.Controllers
{
    [ApiController]
    [EnableCors("ProtocolsCorsPolicy")]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("auth")]
        public IActionResult Auth([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest.Key != "03a3b09a-4365-4eaf-b6e5-9229ef3af106")
            {
                return Unauthorized();
            }

            var accessToken = _tokenService.GenerateAccessToken();
            var refreshToken = _tokenService.GenerateRefreshToken();

            return Ok(new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        [HttpPost("refresh")]
        [Authorize]
        public IActionResult Refresh([FromBody] TokenRequest tokenRequest)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(tokenRequest.AccessToken);
            var newAccessToken = _tokenService.GenerateAccessToken();
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            return Ok(new
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }
    }
}
