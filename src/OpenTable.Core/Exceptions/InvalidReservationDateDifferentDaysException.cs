namespace OpenTable.Core.Exceptions;

public sealed class InvalidReservationDateDifferentDaysException : CustomException
{
    public DateTime From { get; }
    public DateTime To { get; }

    public InvalidReservationDateDifferentDaysException(DateTime from, DateTime to) 
        : base($"Date from: {from} and date to: {to} must be from the same day.")
    {
        From = from;
        To = to;
    }
}