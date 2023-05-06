namespace OpenTable.Core.ValueObjects;

public sealed record OpenTableId
{
    public Guid Value { get; }
    public OpenTableId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidEntityIdException(value);
        }

        Value = value;
    }
    
    public static OpenTableId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(OpenTableId openTableId) => openTableId.Value;
    
    public static implicit operator OpenTableId(Guid value) => new(value);

    public override string ToString() => Value.ToString("N");
}