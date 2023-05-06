namespace OpenTable.Tests.Integration;

internal class TestDatabase : IDisposable
{
    public OpenTableDbContext DbContext { get; }

    public TestDatabase()
    {
        var options = new OptionsProvider().Get<DatabaseOptions>("Database");
        DbContext = new OpenTableDbContext(new DbContextOptionsBuilder<OpenTableDbContext>()
            .UseSqlServer(options.ConnectionString).Options);
    }

    public void Dispose()
    {
        DbContext.Database.EnsureDeleted();
        DbContext?.Dispose();
    }
}