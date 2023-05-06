namespace OpenTable.Infrastructure.DAL;
internal sealed class OpenTableDbContext : DbContext
{
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<WeeklyOpenTable> WeeklyOpenTables { get; set; }
    public DbSet<User> Users { get; set; }

    public OpenTableDbContext(DbContextOptions<OpenTableDbContext> dbContextOptions) 
        : base(dbContextOptions) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}