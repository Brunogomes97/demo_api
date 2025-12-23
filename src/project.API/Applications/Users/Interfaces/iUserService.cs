using project.API.Applications.User.DTOs;
using project.API.Domain.Entities;

namespace project.API.Applications.User.Interfaces;

public interface IUserService
{
    Task<UserEntity> CreateAsync(CreateUserDto dto);
    Task<UserEntity> UpdateAsync(Guid id, UpdateUserDto dto);
    Task<UserEntity> GetByIdAsync(Guid id);
    Task<UserEntity> GetByEmailAsync(string email);
    Task DeleteAsync(Guid id);
}
