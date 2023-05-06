namespace OpenTable.Core.ValueObjects;

public sealed record PhoneNumber
{
    public string Value { get; }

    public PhoneNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyPhoneNumberException();
        }

        if (value.Length is < 8 or > 12)
        {
            throw new InvalidPhoneNumberException(value);
        }
        
        Value = value;
    }

    public static implicit operator string(PhoneNumber phoneNumber) => phoneNumber?.Value;
    public static implicit operator PhoneNumber(string phoneNumber) => new(phoneNumber);
}