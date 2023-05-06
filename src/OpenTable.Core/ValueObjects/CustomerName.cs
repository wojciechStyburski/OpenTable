namespace OpenTable.Core.ValueObjects;

public sealed record CustomerName(string Value)
{
    public string Value { get; } = Value ?? throw new InvalidCustomerNameException();

    public static implicit operator string(CustomerName customerName) => customerName.Value;
    public static implicit operator CustomerName(string customerName) => new (customerName);
}