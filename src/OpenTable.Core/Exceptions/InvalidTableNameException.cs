namespace OpenTable.Core.Exceptions;

public class InvalidTableNameException : CustomException
{
    public InvalidTableNameException() 
        : base("Table name is invalid.")
    {
    }
}