namespace OpenTable.Infrastructure.DAL.Repositories;

internal class InMemoryWeeklyOpenTableRepository : IWeeklyOpenTableRepository
{
    private readonly IClock _clock;
    private readonly List<WeeklyOpenTable> _weeklyOpenTables;

    public InMemoryWeeklyOpenTableRepository(IClock clock)
    {
        _clock = clock;
        _weeklyOpenTables =  new List<WeeklyOpenTable>()        
        {
            WeeklyOpenTable.Create(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(DateOnly.FromDateTime(clock.Current())), "T1", 2),
            WeeklyOpenTable.Create(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(DateOnly.FromDateTime(clock.Current())), "T2", 2),
            WeeklyOpenTable.Create(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(DateOnly.FromDateTime(clock.Current())), "T3", 4),
            WeeklyOpenTable.Create(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(DateOnly.FromDateTime(clock.Current())), "T4", 4),
            WeeklyOpenTable.Create(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(DateOnly.FromDateTime(clock.Current())), "T5", 6),
        };
    }

    public Task<WeeklyOpenTable> GetAsync(OpenTableId id)
        => Task.FromResult(_weeklyOpenTables.SingleOrDefault(x => x.Id == id));

    public Task<IEnumerable<WeeklyOpenTable>> GetAllAsync()
        => Task.FromResult(_weeklyOpenTables.AsEnumerable());

    public Task AddAsync(WeeklyOpenTable weeklyOpenTable)
    {
        _weeklyOpenTables.Add(weeklyOpenTable);
        return Task.CompletedTask;
    }
    public Task UpdateAsync(WeeklyOpenTable weeklyOpenTable) 
        => Task.CompletedTask;
    public Task DeleteAsync(WeeklyOpenTable weeklyOpenTable)
    {
        _weeklyOpenTables.Remove(weeklyOpenTable);
        return Task.CompletedTask;
    }
}