namespace OpenTable.Application.Queries;

public class GetWeeklyTables : IQuery<IEnumerable<WeeklyTableDto>>
{
    public DateTime? Date { get; set; }
}