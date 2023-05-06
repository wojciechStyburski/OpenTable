namespace OpenTable.Application.Exceptions;

public sealed class WeeklyTableNotFoundException : CustomException
{
    public Guid TableId { get; }

    public WeeklyTableNotFoundException()
        : base($"Weekly table was not found.")
    {
    }

    public WeeklyTableNotFoundException(Guid tableId) 
        : base($"Weekly table with id: {tableId} was not found.")
    {
        TableId = tableId;
    }
}