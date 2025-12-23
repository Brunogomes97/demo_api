using project.API.Domain.Entities;
namespace project.API.Applications.Auth.Interfaces;

public interface ITokenService
{
    string Generate(UserEntity user);
}
