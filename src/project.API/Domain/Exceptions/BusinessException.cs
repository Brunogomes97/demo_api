namespace project.API.Domain.Exceptions;

public abstract class BusinessException : Exception
{
    protected BusinessException(string message) : base(message) { }
    public abstract int StatusCode { get; }
}
