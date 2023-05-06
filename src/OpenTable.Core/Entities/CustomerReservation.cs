namespace OpenTable.Core.Entities;

public sealed class CustomerReservation : Reservation
{
    public UserId UserId { get; private set; }
    public CustomerName CustomerName { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }

    private CustomerReservation() { }
    public CustomerReservation
    (
        ReservationId id, 
        OpenTableId openTableId, 
        UserId userId,
        CustomerName customerName, 
        PhoneNumber phoneNumber, 
        OpenTableDateTime from, 
        OpenTableDateTime to,
        Capacity capacity
    ) 
    : base(id, openTableId, from, to, capacity)
    {
        UserId = userId;
        CustomerName = customerName;
        ChangePhoneNumber(phoneNumber);
    }
    
    public void ChangePhoneNumber(PhoneNumber phoneNumber) 
        => PhoneNumber = phoneNumber;
}