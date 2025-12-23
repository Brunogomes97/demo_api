namespace project.API.Domain.Exceptions;

public class ConflictException : BusinessException
{
    public ConflictException(string message) : base(message) { }
    public override int StatusCode => StatusCodes.Status409Conflict;
}
