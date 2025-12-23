namespace project.API.Applications.User.DTOs;

public class UpdateUserDto
{
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? Password { get; set; }
}
