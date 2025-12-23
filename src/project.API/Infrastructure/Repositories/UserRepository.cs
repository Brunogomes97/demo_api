using Microsoft.EntityFrameworkCore;
using project.API.Applications.User.Interfaces;
using project.API.Domain.Entities;
using project.API.Infrastructure.Data;

namespace project.API.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;

    public UserRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task AddAsync(UserEntity user)
    {   
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
    }
    public Task<UserEntity?> GetByIdAsync(Guid id)
        => _db.Users.FirstOrDefaultAsync(u => u.Id == id);

    public Task<UserEntity?> GetByEmailAsync(string email)
        => _db.Users.FirstOrDefaultAsync(u => u.Email == email);

    public Task<UserEntity?> GetByUsernameAsync(string username)
        => _db.Users.FirstOrDefaultAsync(u => u.Username == username);

    public async Task UpdateAsync(UserEntity user)
    {
        _db.Users.Update(user);
        await _db.SaveChangesAsync();
    }
    public async Task DeleteAsync(UserEntity user)
{
    _db.Users.Remove(user);
    await _db.SaveChangesAsync();
}

    public Task<bool> EmailExistsAsync(string email)
        => _db.Users.AnyAsync(u => u.Email == email);
}
