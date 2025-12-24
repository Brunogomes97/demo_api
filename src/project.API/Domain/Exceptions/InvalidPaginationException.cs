namespace project.API.Domain.Exceptions;

public class InvalidPaginationException : BusinessException
{
    public InvalidPaginationException(string message): base(message){ }
    public override int StatusCode => StatusCodes.Status400BadRequest;
    
}