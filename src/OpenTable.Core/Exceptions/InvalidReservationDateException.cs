namespace OpenTable.Core.Exceptions;

public sealed class InvalidReservationDateException : CustomException
{
    public DateTime Date { get; }
    public string DateType { get; }

    public InvalidReservationDateException(DateTime date, string dateType) 
        : base($"Reservation date {dateType}: with value: {date} is invalid.")
    {
        Date = date;
        DateType = dateType;
    }
}