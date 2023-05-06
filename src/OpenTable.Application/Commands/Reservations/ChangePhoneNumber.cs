namespace OpenTable.Application.Commands.Reservations;

public record ChangePhoneNumber(Guid ReservationId, string PhoneNumber) : ICommand;