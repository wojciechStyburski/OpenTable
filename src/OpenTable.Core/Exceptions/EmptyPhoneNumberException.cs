namespace OpenTable.Core.Exceptions;

public sealed class EmptyPhoneNumberException : CustomException
{
    public EmptyPhoneNumberException() 
        : base("Phone number is empty.")
    {
    }
}