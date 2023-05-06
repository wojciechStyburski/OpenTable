namespace OpenTable.Core.ValueObjects;

public sealed record Week
{
    public DateOnly From { get; }
    public DateOnly To { get; }
    
    public Week(DateOnly value)
    {
        var pastDays = value.DayOfWeek is DayOfWeek.Sunday ? 7 : (int) value.DayOfWeek;
        var remainingDays = 7 - pastDays;
        From = value.AddDays(-1 * pastDays);
        To = value.AddDays(remainingDays);
    }

    public override string ToString() => $"{From} -> {To}";
}