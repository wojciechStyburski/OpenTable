namespace OpenTable.Core.Exceptions;

public class InvalidEmailException : CustomException
{
    public string Email { get; }

    public InvalidEmailException(string email) 
        : base($"Email with value: {email} is invalid.")
    {
        Email = email;
    }
}