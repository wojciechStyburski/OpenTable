namespace OpenTable.Application.Exceptions;

public class ReservationNotFoundException : CustomException
{
    public Guid ReservationId { get; }

    public ReservationNotFoundException(Guid reservationId) 
        : base($"Reservation with id: {reservationId} was not found.")
    {
        ReservationId = reservationId;
    }
}