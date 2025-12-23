using project.API.Applications.User.DTOs;
using project.API.Applications.User.Interfaces;
using project.API.Domain.Entities;

namespace project.API.Applications.User.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserEntity> CreateAsync(CreateUserDto dto)
    {
        if (await _repository.EmailExistsAsync(dto.Email))
            throw new InvalidOperationException("Email already registered");

        var user = new UserEntity
        {
            Username = dto.Username,
            Email = dto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        await _repository.AddAsync(user);
        return user;
    }

    public async Task<UserEntity> GetByIdAsync(Guid id)
        => await _repository.GetByIdAsync(id)
           ?? throw new KeyNotFoundException("User not found");

    public async Task<UserEntity> GetByEmailAsync(string email)
        => await _repository.GetByEmailAsync(email)
           ?? throw new KeyNotFoundException("User not found");

    public async Task<UserEntity> UpdateAsync(Guid id, UpdateUserDto dto)
    {
        var user = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("User not found");

        if (user.Email != dto.Email &&
            await _repository.EmailExistsAsync(dto.Email))
            throw new InvalidOperationException("Email already registered");

        user.Username = dto.Username;
        user.Email = dto.Email;

        if (!string.IsNullOrWhiteSpace(dto.Password))
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        }

        await _repository.UpdateAsync(user);
        return user;
    }
    public async Task DeleteAsync(Guid id)
    {
        var user = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("User not found");

        await _repository.DeleteAsync(user);
    }
}
