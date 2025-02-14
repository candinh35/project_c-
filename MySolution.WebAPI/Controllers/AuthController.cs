using Business.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MySolution.WebAPI.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (request is { Username: "admin", Password: "password" })
        {
            var userId = Guid.NewGuid();
            var accessToken = await _authService.GenerateAccessToken(userId);
            var refreshToken = await _authService.GenerateRefreshToken(userId);

            return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken });
        }

        return Unauthorized();
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
    {
        if (await _authService.ValidateRefreshToken(request.UserId, request.RefreshToken))
        {
            var newAccessToken = await _authService.GenerateAccessToken(request.UserId);
            return Ok(new { AccessToken = newAccessToken });
        }

        return Unauthorized();
    }
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class RefreshRequest
{
    public Guid UserId { get; set; }
    public string RefreshToken { get; set; }
}