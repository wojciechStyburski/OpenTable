namespace OpenTable.Application.Commands.Reservations;

public record DeleteReservation(Guid ReservationId) : ICommand;