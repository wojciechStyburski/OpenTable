namespace OpenTable.Core.Entities;

public class WeeklyOpenTable
{
    private readonly HashSet<Reservation> _reservations = new();
    public const int OpenFrom = 10;
    public const int OpenTo = 23;
    public OpenTableId Id { get; private set; }
    public Week Week { get; private set;}
    public TableName Name { get; private set;}
    public Capacity Capacity { get; private set;}
    public IEnumerable<Reservation> Reservations => _reservations;
    
    private WeeklyOpenTable(OpenTableId id, Week week, TableName name, Capacity capacity)
    {
        Id = id;
        Week = week;
        Name = name;
        Capacity = capacity;
    }

    public static WeeklyOpenTable Create(OpenTableId id, Week week, TableName name, Capacity capacity)
        => new(id, week, name, capacity);
    
    internal void AddReservation(Reservation reservation, OpenTableDateTime now)
    {
        if (reservation.From > reservation.To)
        {
            throw new InvalidReservationDateFromGreaterThanToException(reservation.From, reservation.To);
        }

        if (reservation.From.Value.Date != reservation.To.Value.Date)
        {
            throw new InvalidReservationDateDifferentDaysException(reservation.From, reservation.To);
        }

        if (DateOnly.FromDateTime(reservation.From) < Week.From || DateOnly.FromDateTime(reservation.From) > Week.To || reservation.From < now)
        {
            throw new InvalidReservationDateException(reservation.From, nameof(reservation.From));
        }
        
        if (DateOnly.FromDateTime(reservation.To) < Week.From || DateOnly.FromDateTime(reservation.To) > Week.To || reservation.To <  now)
        {
            throw new InvalidReservationDateException(reservation.To, nameof(reservation.To));
        }

        var reservationAlreadyExists = Reservations
            .Any
            (
                x => 
                    (x.From.Value == reservation.From.Value 
                        && x.To.Value == reservation.To.Value)
                    || (reservation.From.Value >= x.From.Value 
                        && reservation.From.Value <= x.To.Value)
                    || (reservation.To.Value >= x.From.Value 
                        && reservation.To.Value <= x.To.Value)
                    || (reservation.From.Value <= x.From.Value 
                        && reservation.To.Value >= x.To.Value)
            );

        if (reservationAlreadyExists)
        {
            throw new TableAlreadyReservedException(Name, reservation.From, reservation.To);
        }

        if (reservation.From.Value.Hour < OpenFrom || reservation.To.Value.Hour > OpenTo)
        {
            throw new InvalidReservationHoursException(OpenFrom, OpenTo);
        }

        if (reservation.Capacity > Capacity)
        {
            throw new TableCapacityExceededException(Id);
        }
        
        _reservations.Add(reservation);
    }

    public void RemoveReservation(Guid id)
    {
        var reservation = _reservations.SingleOrDefault(x => x.Id == new ReservationId(id));
        _reservations.Remove(reservation);
    }
    
    public void RemoveReservations(IEnumerable<Reservation> reservations)
        => _reservations.RemoveWhere(x => reservations.Any(r => r.Id == x.Id));
}