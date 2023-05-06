namespace OpenTable.Infrastructure.DAL.Handlers;

internal sealed class GetWeeklyTablesHandler : IQueryHandler<GetWeeklyTables, IEnumerable<WeeklyTableDto>>
{
    private readonly OpenTableDbContext _dbContext;

    public GetWeeklyTablesHandler(OpenTableDbContext dbContext) 
        => _dbContext = dbContext;

    public async Task<IEnumerable<WeeklyTableDto>> HandleAsync(GetWeeklyTables query)
    {
        var week = query.Date.HasValue ? new Week(DateOnly.FromDateTime(query.Date.Value)) : null;
        var weeklyTables = await _dbContext
            .WeeklyOpenTables
            .Where(x => week == null || x.Week == week)
            .Include(x => x.Reservations)
            .AsNoTracking()
            .ToListAsync();

        return weeklyTables.Select(x => x.AsDto());
    }
}