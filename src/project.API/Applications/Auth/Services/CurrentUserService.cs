using System.IdentityModel.Tokens.Jwt;
using project.API.Applications.Auth.Interfaces;


namespace project.API.Applications.Auth.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _http;

    public CurrentUserService(IHttpContextAccessor http)
    {
        _http = http;
    }

    public Guid UserId =>
        Guid.Parse(_http.HttpContext!
            .User
            .FindFirst(JwtRegisteredClaimNames.Sub)!.Value);

    public string? Email =>
        _http.HttpContext!.User.FindFirst(JwtRegisteredClaimNames.Email)?.Value;

    public string? Username =>
        _http.HttpContext!.User.FindFirst(JwtRegisteredClaimNames.UniqueName)?.Value;
}
