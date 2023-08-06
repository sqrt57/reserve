namespace backend.Exceptions;

public class DbInsertException : Exception
{
    public DbInsertException() : base()
    {
    }

    public DbInsertException(string? message) : base(message)
    {
    }

    public DbInsertException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
