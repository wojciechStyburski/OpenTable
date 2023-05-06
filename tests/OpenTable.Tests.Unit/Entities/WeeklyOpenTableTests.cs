namespace OpenTable.Tests.Unit.Entities;

public class WeeklyOpenTableTests
{
    [Theory]
    [InlineData("2023-04-10 12:00:00", "2023-04-10 13:00:00")]
    [InlineData("2023-04-28 13:00:00", "2023-04-28 15:00:00")]
    public void given_invalid_date_add_reservation_should_fail(string dateStingFrom, string dateStingTo)
    {
        //arrange
        var invalidReservationDateFrom = DateTime.Parse(dateStingFrom);
        var invalidReservationDateTo = DateTime.Parse(dateStingTo);
        var reservation = new CustomerReservation(Guid.NewGuid(), _weeklyOpenTable.Id, Guid.Parse("19cf8a39-2218-4cb1-8d36-3eb29647efd2"),
            "John Doe", "789654123", invalidReservationDateFrom, invalidReservationDateTo, 2);
        
        //act 
        var exception = Record.Exception(() => _weeklyOpenTable.AddReservation(reservation, _now));

        //assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidReservationDateException>();
    }

    [Fact]
    public void given_reservation_for_already_existing_date_add_reservation_should_fail()
    {
        //arrange
        var reservationDateFrom = _now.AddDays(1).AddHours(12);
        var reservationDateTo = _now.AddDays(1).AddHours(13);
        
        var reservation = new CustomerReservation(Guid.NewGuid(), _weeklyOpenTable.Id, Guid.Parse("19cf8a39-2218-4cb1-8d36-3eb29647efd2"), 
            "John Doe", "789654123", reservationDateFrom, reservationDateTo, 2);
        
        _weeklyOpenTable.AddReservation(reservation, _now);
        
        var nextReservation =  new CustomerReservation(Guid.NewGuid(), _weeklyOpenTable.Id, Guid.Parse("19cf8a39-2218-4cb1-8d36-3eb29647efd2"), 
            "John Doe", "789654123", reservationDateFrom, reservationDateTo, 2);
        
        //act 
        var exception = Record.Exception(() => _weeklyOpenTable.AddReservation(nextReservation, _now));
        
        //assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<TableAlreadyReservedException>();
    }

    [Fact]
    public void given_reservation_for_not_taken_datetime_add_reservation_should_succeed()
    {
        //arrange
        var reservationDateFrom = _now.AddDays(1).AddHours(12);
        var reservationDateTo = _now.AddDays(1).AddHours(13);
        
        var reservation = new CustomerReservation(Guid.NewGuid(), _weeklyOpenTable.Id, Guid.Parse("19cf8a39-2218-4cb1-8d36-3eb29647efd2"), 
            "John Doe", "789654123", reservationDateFrom, reservationDateTo, 2);
        
        //act 
        _weeklyOpenTable.AddReservation(reservation, _now);
        
        //assert
        _weeklyOpenTable.Reservations.ShouldHaveSingleItem();
    }
    
    #region Arrange
    
    private readonly DateTime _now;
    private WeeklyOpenTable _weeklyOpenTable;
    
    public WeeklyOpenTableTests()
    {
        _now = new DateTime(2023, 04, 11);
        _weeklyOpenTable =  WeeklyOpenTable.Create(Guid.Parse("887ccee8-2f17-4be8-8307-1219907bb4ae"), new Week(DateOnly.FromDateTime(_now)), "T2", 2);
    }
    
    #endregion
}