using project.API.Applications.User.DTOs;
namespace project.API.Applications.Auth.DTOs;


public class AuthResponseDto
{
    public string Token { get; set; }
    public UserResponseDto User { get; set; }

    public AuthResponseDto(string token, UserResponseDto user)
    {
        Token = token;
        User = user;
    }
}
