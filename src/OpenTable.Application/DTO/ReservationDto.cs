namespace OpenTable.Application.DTO;

public class ReservationDto
{
    public Guid Id { get; set; }
    public Guid OpenTableId { get; set; }
    public string CustomerName { get; set; }
    public string PhoneNumber { get;  set; }
    public DateTime From { get;  set; }
    public DateTime To { get;  set; }
    
    public string Type { get; set; }
}