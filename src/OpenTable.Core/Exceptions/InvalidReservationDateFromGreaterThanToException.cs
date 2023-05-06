namespace OpenTable.Core.Exceptions;

public sealed class InvalidReservationDateFromGreaterThanToException : CustomException
{
    public DateTime From { get; }
    public DateTime To { get; }

    public InvalidReservationDateFromGreaterThanToException(DateTime from, DateTime to) 
        : base($"Reservation date from: {from} is greater than {to}.")
    {
        From = from;
        To = to;
    }
}