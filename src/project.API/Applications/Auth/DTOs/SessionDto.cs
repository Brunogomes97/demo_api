using project.API.Applications.User.DTOs;
namespace project.API.Applications.Auth.DTOs;


public class SessionResponseDto
{
    public UserResponseDto User { get; set; }

    public SessionResponseDto(UserResponseDto user)
    {
        User = user;
    }
}
