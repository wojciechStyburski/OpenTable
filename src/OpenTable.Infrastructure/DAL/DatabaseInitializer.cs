namespace OpenTable.Infrastructure.DAL;

internal sealed class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseInitializer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<OpenTableDbContext>();
        await dbContext.Database.MigrateAsync(cancellationToken);
        
        var clock = new Clock();
        var currentWeek = new Week(DateOnly.FromDateTime(clock.Current()));
        var weeklyOpenTables = dbContext.WeeklyOpenTables.Where(x => x.Week == currentWeek).ToList();
        if (!weeklyOpenTables.Any())
        {
            
            weeklyOpenTables = new List<WeeklyOpenTable>()        
            {
                WeeklyOpenTable.Create(Guid.NewGuid(), currentWeek, "Table 1", 2),
                WeeklyOpenTable.Create(Guid.NewGuid(), currentWeek, "Table 2", 2),
                WeeklyOpenTable.Create(Guid.NewGuid(), currentWeek, "Table 3", 4),
                WeeklyOpenTable.Create(Guid.NewGuid(), currentWeek, "Table 4", 4),
                WeeklyOpenTable.Create(Guid.NewGuid(), currentWeek, "Table 5", 6),
            };
        
            dbContext.WeeklyOpenTables.AddRange(weeklyOpenTables);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}