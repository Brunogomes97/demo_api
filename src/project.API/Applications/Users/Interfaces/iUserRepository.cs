using project.API.Domain.Entities;
namespace project.API.Applications.User.Interfaces;

public interface IUserRepository
{
    Task AddAsync(UserEntity user);
    Task<UserEntity?> GetByIdAsync(Guid id);
    Task<UserEntity?> GetByEmailAsync(string email);
    Task<UserEntity?> GetByUsernameAsync(string username);
    Task UpdateAsync(UserEntity user);
    Task DeleteAsync(UserEntity user);
    Task<bool> EmailExistsAsync(string email);
}
