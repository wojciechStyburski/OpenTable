namespace OpenTable.Core.ValueObjects;

public sealed record TableName(string Value)
{
    public string Value { get; } = Value ?? throw new InvalidTableNameException();
    
    public static implicit operator string(TableName tableName) => tableName.Value;
    public static implicit operator TableName(string tableName) => new(tableName);
}