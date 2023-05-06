namespace OpenTable.Application.Exceptions;

public class InvalidCredentialsException : CustomException
{
    public InvalidCredentialsException() 
        : base("Invalid credentials.")
    {
    }
}