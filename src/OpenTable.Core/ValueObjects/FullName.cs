namespace OpenTable.Core.ValueObjects;

public sealed record FullName
{
    public string Value { get; }

    public FullName(string value)
    {
        if (string.IsNullOrEmpty(value) || value.Length > 150)
            throw new InvalidFullNameException(value);

        Value = value;
    }

    public static implicit operator string(FullName name) => name.Value;
    public static implicit operator FullName(string value) => new(value);
}