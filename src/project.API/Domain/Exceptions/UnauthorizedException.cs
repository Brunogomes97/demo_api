namespace project.API.Domain.Exceptions;

public class UnauthorizedException : BusinessException
{
    public UnauthorizedException(string message) : base(message) { }

    public override int StatusCode => StatusCodes.Status401Unauthorized;
}
