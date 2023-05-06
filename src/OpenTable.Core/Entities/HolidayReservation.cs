namespace OpenTable.Core.Entities;

public class HolidayReservation : Reservation
{
    private HolidayReservation() { }
    public HolidayReservation(ReservationId id, OpenTableId openTableId, OpenTableDateTime from, OpenTableDateTime to, Capacity capacity) 
        : base(id, openTableId, from, to, capacity) { }
}