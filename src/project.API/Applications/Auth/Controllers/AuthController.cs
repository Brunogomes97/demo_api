using Microsoft.AspNetCore.Mvc;
using project.API.Applications.Auth.DTOs;
using project.API.Applications.Auth.Interfaces;

namespace project.API.Auth.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;

    public AuthController(IAuthService auth)
    {
        _auth = auth;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp(SignUpDto dto)
    {
        await _auth.SignUpAsync(dto);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPost("signin")]
    public async Task<ActionResult<AuthResponseDto>> SignIn(SignInDto dto)
    {
        var result = await _auth.SignInAsync(dto);

        return Ok(result);
    }
}
