namespace backend.Exceptions;

public class DbUpdateException : Exception
{
    public DbUpdateException() : base()
    {
    }

    public DbUpdateException(string? message) : base(message)
    {
    }

    public DbUpdateException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
