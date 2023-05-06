namespace OpenTable.Infrastructure.DAL.Repositories;

internal sealed class WeeklyOpenTableRepository : IWeeklyOpenTableRepository
{
    private readonly OpenTableDbContext _dbContext;

    public WeeklyOpenTableRepository(OpenTableDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<WeeklyOpenTable> GetAsync(OpenTableId id) 
        =>_dbContext
            .WeeklyOpenTables
            .Include(x => x.Reservations)
            .SingleOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<WeeklyOpenTable>> GetAllAsync()
    {   
        var result = await _dbContext
            .WeeklyOpenTables
            .Include(x => x.Reservations)
            .ToListAsync();

        return result.AsEnumerable();
    }

    public async Task<IEnumerable<WeeklyOpenTable>> GetByWeekAsync(Week week)
    {
        var result = await _dbContext
            .WeeklyOpenTables
            .Include(x => x.Reservations)
            .Where(x => x.Week == week)
            .ToListAsync();

        return result;
    }

    public async Task AddAsync(WeeklyOpenTable weeklyOpenTable)
    {
        await _dbContext.AddAsync(weeklyOpenTable);
    }

    public Task UpdateAsync(WeeklyOpenTable weeklyOpenTable)
    {
        _dbContext.Update(weeklyOpenTable);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(WeeklyOpenTable weeklyOpenTable)
    {
        _dbContext.Remove(weeklyOpenTable);
        return Task.CompletedTask;
    }
}