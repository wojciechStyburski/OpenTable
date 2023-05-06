namespace OpenTable.Core.Exceptions;

public class InvalidReservationHoursException : CustomException
{
    private readonly int _from;
    private readonly int _to;

    public InvalidReservationHoursException(int from, int to) 
        : base($"Invalid reservation hours, reservation outside of available restaurant opening hours between {from} and {to}.")
    {
        _from = from;
        _to = to;
    }
}