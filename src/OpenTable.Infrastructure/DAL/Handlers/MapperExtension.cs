namespace OpenTable.Infrastructure.DAL.Handlers;

internal static class MapperExtension
{
    public static WeeklyTableDto AsDto(this WeeklyOpenTable entity) => new()
    {
        Id = entity.Id.ToString(),
        Name = entity.Name,
        From = entity.Week.From,
        To = entity.Week.To,
        Capacity = entity.Capacity,
        Reservations = entity.Reservations.Select(x => new ReservationDto()
        {
            Id = x.Id,
            OpenTableId = x.OpenTableId,
            CustomerName = x is CustomerReservation cn ? cn.CustomerName : string.Empty,
            PhoneNumber = x is CustomerReservation pn ? pn.PhoneNumber : string.Empty,
            Type = x is CustomerReservation t ? "customer": "holiday",
            From = x.From,
            To = x.To
        })
    };

    public static UserDto AsDto(this User entity) => new()
    {
        Id = entity.Id,
        UserName = entity.UserName,
        FullName = entity.FullName
    };
}