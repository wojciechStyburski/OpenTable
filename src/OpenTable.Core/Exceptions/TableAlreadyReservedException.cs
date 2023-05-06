namespace OpenTable.Core.Exceptions;

public sealed class TableAlreadyReservedException : CustomException
{
    public string Name { get; }
    public DateTime From { get; }
    public DateTime To { get; }

    public TableAlreadyReservedException(string name, DateTime from, DateTime to) 
        : base($"Table: {name} is already reserved from: {from}, to: {to}.")
    {
        Name = name;
        From = from;
        To = to;
    }
}