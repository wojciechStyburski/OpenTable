namespace OpenTable.Application.Commands.Reservations;

public record ReserveTableForCustomer(Guid ReservationId, Guid OpenTableId, Guid UserId,
    string CustomerName, string PhoneNumber, DateTime From, DateTime To, int Capacity) : ICommand;