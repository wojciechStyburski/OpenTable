namespace OpenTable.Core.DomainServices;

public sealed class TableReservationService : ITableReservationService
{
    private readonly IClock _clock;

    public TableReservationService(IClock clock)
    {
        _clock = clock;
    }

    public void ReserveTableForCustomer(WeeklyOpenTable tableToReserve, CustomerReservation reservation)
    {
        tableToReserve.AddReservation(reservation, new OpenTableDateTime(_clock.Current()));
    }

    public void ReserveTablesForHolidays(IEnumerable<WeeklyOpenTable> allTables, DateOnly date)
    {
        foreach (var table in allTables)
        {
            var reservationsForSameDate = table
                .Reservations
                .Where(x => DateOnly.FromDateTime(x.From.Value) == date);
            
            table.RemoveReservations(reservationsForSameDate);
            
            var holidayReservation = new HolidayReservation
            (
                ReservationId.Create(), 
                table.Id,
                new OpenTableDateTime(new DateTime(date.Year, date.Month, date.Day).AddHours(WeeklyOpenTable.OpenFrom)),
                new OpenTableDateTime(new DateTime(date.Year, date.Month, date.Day).AddHours(WeeklyOpenTable.OpenTo)), 2
            );
            
            table.AddReservation(holidayReservation, _clock.Current());
        }
    }
}