using project.API.Applications.Auth.DTOs;
using project.API.Applications.User.DTOs;
using project.API.Applications.Auth.Interfaces;
using project.API.Applications.User.Interfaces;
using project.API.Domain.Entities;
using project.API.Domain.Exceptions;

namespace project.API.Applications.Auth.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _users;
    private readonly ITokenService _tokenService;

    public AuthService(
        IUserRepository users,
        ITokenService tokenService)
    {
        _users = users;
        _tokenService = tokenService;
    }

    public async Task SignUpAsync(SignUpDto dto)
    {
        var exists = await _users.EmailExistsAsync(dto.Email);
        if (exists)
            throw new InvalidOperationException("User already exists");

        var user = new UserEntity
        {
            Username = dto.Username,
            Email = dto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };
        await _users.AddAsync(user);
    }

    public async Task<AuthResponseDto> SignInAsync(SignInDto dto)
    {
        var user = await _users.GetByEmailAsync(dto.Email);
        Console.WriteLine($"User encontrado: {user?.Id} | {user?.Email}");

        if (user == null ||
            !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
        {
            throw new UnauthorizedException("Invalid credentials");
        }

        var token = _tokenService.Generate(user);
        var userDto = new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };
        return new AuthResponseDto(token, userDto);
    }
}
