namespace OpenTable.Core.Exceptions;

public sealed class InvalidCustomerNameException : CustomException
{
    public InvalidCustomerNameException() 
        : base("Customer name is invalid.")
    {
    }
}