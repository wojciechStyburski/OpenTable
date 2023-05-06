namespace OpenTable.Core.ValueObjects;

public sealed record OpenTableDateTime(DateTime Value)
{
    public DateTime AddDays(int days) => new(Value.Year, Value.Month, Value.Day + days);
    
    public static implicit operator OpenTableDateTime(DateTime dateTime) => new(dateTime);

    public static implicit operator DateTime(OpenTableDateTime dateTime) => dateTime.Value;
    
    public static bool operator <(OpenTableDateTime date1, OpenTableDateTime date2) => date1.Value < date2.Value;

    public static bool operator >(OpenTableDateTime date1, OpenTableDateTime date2) => date1.Value > date2.Value;

    public static bool operator <=(OpenTableDateTime date1, OpenTableDateTime date2) => date1.Value <= date2.Value;
    
    public static bool operator >=(OpenTableDateTime date1, OpenTableDateTime date2)=> date1.Value >= date2.Value;
    
    public override string ToString() => Value.ToString("d");
}