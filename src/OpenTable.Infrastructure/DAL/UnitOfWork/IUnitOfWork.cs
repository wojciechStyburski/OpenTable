namespace OpenTable.Infrastructure.DAL.UnitOfWork;

internal interface IUnitOfWork
{
    Task ExecuteAsync(Func<Task> action);
}