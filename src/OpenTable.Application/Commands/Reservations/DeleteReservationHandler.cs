namespace OpenTable.Application.Commands.Reservations;

public class DeleteReservationHandler : ICommandHandler<DeleteReservation>
{
    private readonly IWeeklyOpenTableRepository _weeklyOpenTableRepository;

    public DeleteReservationHandler(IWeeklyOpenTableRepository weeklyOpenTableRepository) 
        => _weeklyOpenTableRepository = weeklyOpenTableRepository;

    public async Task HandleAsync(DeleteReservation command)
    {
        var weeklyOpenTables = await _weeklyOpenTableRepository.GetAllAsync();
        var weeklyOpenTable = weeklyOpenTables
            .SingleOrDefault(x => x.Reservations
                .Any(r => r.Id.Value == command.ReservationId));
        
        if (weeklyOpenTable is null)
        {
            throw new WeeklyTableNotFoundException();
        }
        
        weeklyOpenTable.RemoveReservation(command.ReservationId);
        await _weeklyOpenTableRepository.UpdateAsync(weeklyOpenTable);
    }
}