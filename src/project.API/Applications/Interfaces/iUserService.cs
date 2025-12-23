using project.API.Applications.DTOs;
using project.API.Domain.Entities;

namespace project.API.Applications.Services;

public interface IUserService
{
    Task<User> CreateAsync(CreateUserDto dto);
    Task<User> UpdateAsync(Guid id, UpdateUserDto dto);
    Task<User> GetByIdAsync(Guid id);
    Task<User> GetByEmailAsync(string email);
    Task DeleteAsync(Guid id);
}
