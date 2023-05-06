namespace OpenTable.Application.Commands.Reservations;

public class ReserveTableForHolidayHandler : ICommandHandler<ReserveTableForHoliday>
{
    private readonly IWeeklyOpenTableRepository _weeklyOpenTableRepository;
    private readonly ITableReservationService _tableReservationService;

    public ReserveTableForHolidayHandler(IWeeklyOpenTableRepository weeklyOpenTableRepository, ITableReservationService tableReservationService)
    {
        _weeklyOpenTableRepository = weeklyOpenTableRepository;
        _tableReservationService = tableReservationService;
    }

    public async Task HandleAsync(ReserveTableForHoliday command)
    {
        var week = new Week(command.Date);
        var openTables = (await _weeklyOpenTableRepository.GetByWeekAsync(week)).ToList();
        _tableReservationService.ReserveTablesForHolidays(openTables, command.Date);

        var tasks = openTables.Select(x => _weeklyOpenTableRepository.UpdateAsync(x));
        await Task.WhenAll(tasks);
    }
}