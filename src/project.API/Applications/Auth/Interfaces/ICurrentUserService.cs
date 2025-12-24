namespace project.API.Applications.Auth.Interfaces;

public interface ICurrentUserService
{
    Guid UserId { get; }
    string? Email { get; }
    string? Username { get; }
}
