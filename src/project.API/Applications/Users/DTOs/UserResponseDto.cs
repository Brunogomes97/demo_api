namespace project.API.Applications.User.DTOs;

public class UserResponseDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
     public DateTime? CreatedAt { get; set; }
}
