namespace OpenTable.Core.ValueObjects;

public sealed record Password
{
    public string Value { get; }

    public Password(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new InvalidPasswordException();

        Value = value;
    }

    public static implicit operator Password(string value) => new(value);
    public static implicit operator string(Password value) => value.Value;
    public override string ToString() => Value;
}