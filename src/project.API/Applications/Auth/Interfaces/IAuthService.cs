using project.API.Applications.Auth.DTOs;
namespace project.API.Applications.Auth.Interfaces;
public interface IAuthService
{
    Task SignUpAsync(SignUpDto dto);
    Task<AuthResponseDto> SignInAsync(SignInDto dto);
}