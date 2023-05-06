namespace OpenTable.Infrastructure.DAL.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasConversion(x => x.Value, x => new UserId(x));

        builder
            .HasIndex(x => x.Email)
            .IsUnique();
        builder
            .Property(x => x.Email)
            .HasConversion(x => x.Value, x => new Email(x))
            .IsRequired()
            .HasMaxLength(200);

        builder
            .HasIndex(x => x.UserName)
            .IsUnique();

        builder
            .Property(x => x.UserName)
            .HasConversion(x => x.Value, x => new UserName(x))
            .IsRequired()
            .HasMaxLength(50);
        
        builder
            .Property(x => x.FullName)
            .HasConversion(x => x.Value, x => new FullName(x))
            .IsRequired()
            .HasMaxLength(150);

        builder
            .Property(x => x.Password)
            .HasConversion(x => x.Value, x => new Password(x))
            .IsRequired()
            .HasMaxLength(200);

        builder
            .Property(x => x.Role)
            .HasConversion(x => x.Value, x => new Role(x))
            .IsRequired()
            .HasMaxLength(20);

        builder
            .Property(x => x.CreatedAt)
            .IsRequired();
    }
}