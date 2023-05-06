namespace OpenTable.Core.Repositories;

public interface IWeeklyOpenTableRepository
{
    Task<WeeklyOpenTable> GetAsync(OpenTableId id);
    Task<IEnumerable<WeeklyOpenTable>> GetAllAsync();
    Task<IEnumerable<WeeklyOpenTable>> GetByWeekAsync(Week week) => throw new NotImplementedException();
    Task AddAsync(WeeklyOpenTable weeklyOpenTable);
    Task UpdateAsync(WeeklyOpenTable weeklyOpenTable);
    Task DeleteAsync(WeeklyOpenTable weeklyOpenTable);
    
}