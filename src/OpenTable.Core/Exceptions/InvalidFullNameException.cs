namespace OpenTable.Core.Exceptions;

public class InvalidFullNameException : CustomException
{
    public string FullName { get; }

    public InvalidFullNameException(string fullName) 
        : base($"Full name with value: {fullName} is invalid.")
    {
        FullName = fullName;
    }
}