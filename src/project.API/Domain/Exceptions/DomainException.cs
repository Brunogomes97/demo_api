namespace project.API.Domain.Exceptions;

public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message){ }
    public abstract int StatusCode { get; }
}
