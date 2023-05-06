namespace OpenTable.Infrastructure.DAL.Configurations;

internal sealed class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasConversion(x => x.Value, x => new ReservationId(x));
        
        builder
            .Property(x => x.OpenTableId)
            .HasConversion(x => x.Value, x => new OpenTableId(x));
        
        builder
            .Property(x => x.From)
            .HasConversion(x => x.Value, x => new OpenTableDateTime(x));
        
        builder
            .Property(x => x.To)
            .HasConversion(x => x.Value, x => new OpenTableDateTime(x));
        
        builder
            .Property(x => x.Capacity)
            .IsRequired()
            .HasConversion(x => x.Value, x => new Capacity(x));

        builder
            .HasDiscriminator<string>("Type")
            .HasValue<HolidayReservation>(nameof(HolidayReservation))
            .HasValue<CustomerReservation>(nameof(CustomerReservation));
        
        
    }
}