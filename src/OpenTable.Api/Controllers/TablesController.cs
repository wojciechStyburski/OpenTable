namespace OpenTable.Api.Controllers;

[Route("tables")]
[ApiController]
public class TablesController : ControllerBase
{
    
    private readonly ICommandHandler<ReserveTableForCustomer> _reserveTableForCustomer;
    private readonly ICommandHandler<ReserveTableForHoliday> _reserveTableForHoliday;
    private readonly ICommandHandler<ChangePhoneNumber> _changePhoneNumber;
    private readonly ICommandHandler<DeleteReservation> _deleteReservation;
    private readonly IQueryHandler<GetWeeklyTables, IEnumerable<WeeklyTableDto>> _getWeeklyTables;

    public TablesController
    (
        ICommandHandler<ReserveTableForCustomer> reserveTableForCustomer,
        ICommandHandler<ReserveTableForHoliday> reserveTableForHoliday, 
        ICommandHandler<ChangePhoneNumber> changePhoneNumber, 
        ICommandHandler<DeleteReservation> deleteReservation,
        IQueryHandler<GetWeeklyTables, IEnumerable<WeeklyTableDto>> getWeeklyTables
    )
    {
        _reserveTableForCustomer = reserveTableForCustomer;
        _getWeeklyTables = getWeeklyTables;
        _reserveTableForHoliday = reserveTableForHoliday;
        _changePhoneNumber = changePhoneNumber;
        _deleteReservation = deleteReservation;
    }
    
    [HttpGet]
    [SwaggerOperation("Get tables details")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Reservation>>> Get([FromQuery] GetWeeklyTables query) 
        => Ok(await _getWeeklyTables.HandleAsync(query));
    
    [HttpPost("{tableId:guid}/reservations/customer")]
    [Authorize]
    [SwaggerOperation("Reserve table for customer")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Post(Guid tableId, ReserveTableForCustomer command)
    {
        await _reserveTableForCustomer.HandleAsync(command with
        {
            ReservationId = Guid.NewGuid(),
            OpenTableId = tableId
        });

        return NoContent();
    }

    [HttpPost("reservations/holiday")]
    [Authorize(Policy = "is-admin")]
    [SwaggerOperation("Reserve table for holidays")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Post(ReserveTableForHoliday command)
    {
        await _reserveTableForHoliday.HandleAsync(command);
        return NoContent();
    }
    
    [HttpPut("reservations/{reservationId:guid}")]
    [Authorize]
    [SwaggerOperation("Change reservation phone number")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Put(Guid reservationId, ChangePhoneNumber command)
    {
        await _changePhoneNumber.HandleAsync(command with { ReservationId = reservationId });
        return NoContent();
    }

    [HttpDelete("reservations/{reservationId:guid}")]
    [Authorize]
    [SwaggerOperation("Delete reservation")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid reservationId)
    {
        await _deleteReservation.HandleAsync(new DeleteReservation(reservationId));
        return NoContent();
    }
}