namespace OpenTable.Infrastructure.DAL.UnitOfWork;

internal sealed class MssqlUnitOfWork : IUnitOfWork
{
    private readonly OpenTableDbContext _dbContext;

    public MssqlUnitOfWork(OpenTableDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task ExecuteAsync(Func<Task> action)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            await action();
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch(Exception e)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}