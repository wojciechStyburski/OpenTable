namespace OpenTable.Application.Commands.Reservations;

public class ChangePhoneNumberHandler : ICommandHandler<ChangePhoneNumber>
{
    private readonly IWeeklyOpenTableRepository _weeklyOpenTableRepository;

    public ChangePhoneNumberHandler(IWeeklyOpenTableRepository weeklyOpenTableRepository)
    {
        _weeklyOpenTableRepository = weeklyOpenTableRepository;
    }

    public async Task HandleAsync(ChangePhoneNumber command)
    {
        var weeklyOpenTables = await _weeklyOpenTableRepository.GetAllAsync();
        var weeklyOpenTable = weeklyOpenTables
            .SingleOrDefault(x => x.Reservations
                .Any(r => r.Id.Value == command.ReservationId));
        
        if (weeklyOpenTable is null)
        {
            throw new WeeklyTableNotFoundException();
        }

        var reservationId = new ReservationId(command.ReservationId);
        var reservation = weeklyOpenTable
            .Reservations
            .OfType<CustomerReservation>()
            .SingleOrDefault(x => x.Id == reservationId);

        if (reservation is null)
        {
            throw new ReservationNotFoundException(command.ReservationId);
        }
        
        reservation.ChangePhoneNumber(command.PhoneNumber);
        await _weeklyOpenTableRepository.UpdateAsync(weeklyOpenTable);
    }
}