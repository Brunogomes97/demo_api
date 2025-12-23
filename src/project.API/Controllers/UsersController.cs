using Microsoft.AspNetCore.Mvc;
using project.API.Applications.DTOs;
using project.API.Applications.Services;
using project.API.Domain.Entities;

namespace project.API.Controllers;

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
    public async Task<ActionResult<UserResponseDto>> GetById(Guid id)
    {
        var user = await _service.GetByIdAsync(id);
        return Ok(MapToResponse(user));
    }

    [HttpGet("by-email/{email}")]
    public async Task<ActionResult<UserResponseDto>> GetByEmail(string email)
    {
        var user = await _service.GetByEmailAsync(email);
        return Ok(MapToResponse(user));
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<UserResponseDto>> Update(
        Guid id,
        [FromBody] UpdateUserDto dto)
    {
        var user = await _service.UpdateAsync(id, dto);
        return Ok(MapToResponse(user));
    }

    private static UserResponseDto MapToResponse(User user)
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
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
