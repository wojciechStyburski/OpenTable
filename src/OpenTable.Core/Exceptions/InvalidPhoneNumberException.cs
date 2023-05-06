namespace OpenTable.Core.Exceptions;

public class InvalidPhoneNumberException : CustomException
{
    public string PhoneNumber { get; }

    public InvalidPhoneNumberException(string phoneNumber) 
        : base($"Phone number: {phoneNumber} is invalid.")
    {
        PhoneNumber = phoneNumber;
    }
}