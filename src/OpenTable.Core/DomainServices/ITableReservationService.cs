namespace OpenTable.Core.DomainServices;

public interface ITableReservationService
{
    void ReserveTableForCustomer(WeeklyOpenTable tableToReserve, CustomerReservation reservation);
    void ReserveTablesForHolidays(IEnumerable<WeeklyOpenTable> allTables, DateOnly date);
}