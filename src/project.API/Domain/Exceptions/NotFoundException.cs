namespace project.API.Domain.Exceptions;

public class NotFoundException : BusinessException
{
    public NotFoundException(string message) : base(message) { }
    public override int StatusCode => StatusCodes.Status404NotFound;
}
