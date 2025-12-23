namespace project.API.Applications.Auth.DTOs;

public class SignUpDto
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
