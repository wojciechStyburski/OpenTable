namespace OpenTable.Application.Commands.Reservations;

public sealed class ReserveTableForCustomerHandler : ICommandHandler<ReserveTableForCustomer>
{
    private readonly IWeeklyOpenTableRepository _weeklyOpenTableRepository;
    private readonly ITableReservationService _tableReservationService;
    private readonly IUserRepository _userRepository;

    public ReserveTableForCustomerHandler
    (
        IWeeklyOpenTableRepository weeklyOpenTableRepository, 
        ITableReservationService tableReservationService, 
        IUserRepository userRepository
    )
    {
        _weeklyOpenTableRepository = weeklyOpenTableRepository;
        _tableReservationService = tableReservationService;
        _userRepository = userRepository;
    }

    public async Task HandleAsync(ReserveTableForCustomer command)
    {
        var openTableId = new OpenTableId(command.OpenTableId);
        var openTableToReserve = await _weeklyOpenTableRepository.GetAsync(openTableId);
        
        if (openTableToReserve is null)
        {
            throw new WeeklyTableNotFoundException(command.OpenTableId);
        }

        var user = await _userRepository.GetByIdAsync(command.UserId);
        if (user is null)
        {
            return;
        }
        
        var reservation = new CustomerReservation
        (
            command.ReservationId, command.OpenTableId, user.Id, command.CustomerName, 
            command.PhoneNumber, command.From, command.To, command.Capacity
        );
        
        _tableReservationService.ReserveTableForCustomer(openTableToReserve, reservation);
        await _weeklyOpenTableRepository.UpdateAsync(openTableToReserve);
    }
}