namespace OpenTable.Infrastructure.Logging;

public static class Extensions
{
    internal static IServiceCollection AddCustomLogging(this IServiceCollection services)
    {
        services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));
        
        return services;
    }

    public static WebApplicationBuilder UseSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration
                .WriteTo.Console()
                .WriteTo.File($"logs/log-{DateTime.UtcNow.Date:yyyy-MM-dd}.txt")
                .WriteTo.Seq("http://localhost:5341");
        });

        return builder;
    }
}