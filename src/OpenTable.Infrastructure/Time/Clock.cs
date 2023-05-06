namespace OpenTable.Infrastructure.Time;

public class Clock : IClock
{
    public DateTime Current() => DateTime.UtcNow;
}