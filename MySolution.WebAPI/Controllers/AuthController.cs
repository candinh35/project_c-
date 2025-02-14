using Business.Core.Contracts;
using Business.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MySolution.WebAPI.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : Controller
{
    private readonly IAuthService _authService;
    private readonly IUsers _userService;

    public AuthController(IAuthService authService, IUsers users)
    {
        _authService = authService;
        _userService = users;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthLoginRequest request)
    {
        var res = await _authService.Login(request);

        if (res is { AccessToken: null })
        {
            return Unauthorized();
        }

        return Ok(res);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] string refreshToken)
    {
        var res = await _authService.Refresh(refreshToken);
        if (res is { AccessToken: null })
        {
            return Unauthorized();
        }

        return Ok(res);
    }
}