using Microsoft.AspNetCore.Mvc;
using project.API.Applications.User.DTOs;
using project.API.Applications.User.Interfaces;
using project.API.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
namespace project.API.User.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
    {
        var user = await _service.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = user.Id },
            MapToResponse(user)
        );
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<ActionResult<UserResponseDto>> GetById(Guid id)
    {
        var user = await _service.GetByIdAsync(id);
        return Ok(MapToResponse(user));
    }

    [HttpGet("by-email/{email}")]
    [Authorize]
    public async Task<ActionResult<UserResponseDto>> GetByEmail(string email)
    {
        var user = await _service.GetByEmailAsync(email);
        return Ok(MapToResponse(user));
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<ActionResult<UserResponseDto>> Update(
        Guid id,
        [FromBody] UpdateUserDto dto)
    {
        var user = await _service.UpdateAsync(id, dto);
        return Ok(MapToResponse(user));
    }

    private static UserResponseDto MapToResponse(UserEntity user)
    {
        return new UserResponseDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            CreatedAt = user.CreatedAt
        };
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
