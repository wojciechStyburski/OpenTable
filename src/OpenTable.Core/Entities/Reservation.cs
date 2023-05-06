namespace OpenTable.Core.Entities;

public abstract class Reservation
{
    public ReservationId Id { get; }
    public OpenTableId OpenTableId { get; private set; }
    public OpenTableDateTime From { get; private set; }
    public OpenTableDateTime To { get; private set; }
    public Capacity Capacity { get; private set; }

    protected Reservation() { }
    protected Reservation(ReservationId id, OpenTableId openTableId, OpenTableDateTime from, OpenTableDateTime to, Capacity capacity)
    {
        Id = id;
        OpenTableId = openTableId;
        From = from;
        To = to;
        Capacity = capacity;
    }

    
}