namespace backend.Exceptions;

public class UserIdNotAvailableException : Exception
{
    public UserIdNotAvailableException() : base()
    {
    }

    public UserIdNotAvailableException(string? message) : base(message)
    {
    }

    public UserIdNotAvailableException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
