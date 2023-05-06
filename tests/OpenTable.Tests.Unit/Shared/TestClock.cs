namespace OpenTable.Tests.Unit.Shared;

public class TestClock : IClock
{
    public DateTime Current() => new(2023, 04, 18, 12, 0, 0);
}