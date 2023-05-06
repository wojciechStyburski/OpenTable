namespace OpenTable.Infrastructure.DAL.Configurations;

internal sealed class WeeklyOpenTableConfiguration : IEntityTypeConfiguration<WeeklyOpenTable>
{
    public void Configure(EntityTypeBuilder<WeeklyOpenTable> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder
            .Property(x => x.Id)
            .HasConversion(x => x.Value, x => new OpenTableId(x));
        
        builder
            .Property(x => x.Week)
            .HasConversion(
                x => new DateTime(x.To.Year, x.To.Month, x.To.Day), 
                x => new Week(DateOnly.FromDateTime(x)));
        
        builder
            .Property(x => x.Capacity)
            .HasConversion(x => x.Value, x => new Capacity(x));
        
        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasConversion(x => x.Value, x => new TableName(x));
    }
}