namespace OpenTable.Core.ValueObjects;

public sealed record Role
{
    public string Value { get; }
    public Role(string value)
    {
        if (string.IsNullOrEmpty(value) || value.Length > 20)
            throw new InvalidRoleException(value);

        Value = value;
    }

    public static implicit operator Role(string value) => new(value);
    public static implicit operator string(Role value) => value?.Value;
    public override string ToString() => Value;
}