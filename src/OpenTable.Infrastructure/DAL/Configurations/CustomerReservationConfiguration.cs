namespace OpenTable.Infrastructure.DAL.Configurations;

internal sealed class CustomerReservationConfiguration : IEntityTypeConfiguration<CustomerReservation>
{
    public void Configure(EntityTypeBuilder<CustomerReservation> builder)
    {
        builder.Property(x => x.UserId)
            .IsRequired()
            .HasConversion(x => x.Value, x => new UserId(x));
        
        builder
            .Property(x => x.CustomerName)
            .HasConversion(x => x.Value, x => new CustomerName(x));
        
        builder
            .Property(x => x.PhoneNumber)
            .HasConversion(x => x.Value, x => new PhoneNumber(x));
    }
}