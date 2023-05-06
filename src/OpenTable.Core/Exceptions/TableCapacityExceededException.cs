namespace OpenTable.Core.Exceptions;

public sealed class TableCapacityExceededException : CustomException
{
    public OpenTableId TableId { get; }

    public TableCapacityExceededException(OpenTableId tableId) 
        : base($"Table with id {tableId} exceeds its reservation capacity.")
    {
        TableId = tableId;
    }
}