namespace OpenTable.Application.Commands.Reservations;

public record ReserveTableForHoliday(DateOnly Date) : ICommand;
