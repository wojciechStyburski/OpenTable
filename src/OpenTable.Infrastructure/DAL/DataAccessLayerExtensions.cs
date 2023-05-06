namespace OpenTable.Infrastructure.DAL;

internal static class DataAccessLayerExtensions
{
    private const string DatabaseSectionName = "Database";
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(DatabaseSectionName);
        services.Configure<DatabaseOptions>(section);
        var options = configuration.GetOptions<DatabaseOptions>(DatabaseSectionName);
        
        services.AddDbContext<OpenTableDbContext>(x => x.UseSqlServer(options.ConnectionString));
        services.AddScoped<IWeeklyOpenTableRepository, WeeklyOpenTableRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, MssqlUnitOfWork>();
        services.TryDecorate(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));
        
        services.AddHostedService<DatabaseInitializer>();
        
        return services;
    }


}