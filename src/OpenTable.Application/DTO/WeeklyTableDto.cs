namespace OpenTable.Application.DTO;

public class WeeklyTableDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public DateOnly From { get; set; }
    public DateOnly To { get; set; }
    public int Capacity { get; set; }
    public IEnumerable<ReservationDto> Reservations { get; set; }
}