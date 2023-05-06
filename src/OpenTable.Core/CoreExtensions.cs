namespace OpenTable.Core;

public static class CoreExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
        => services
            .AddSingleton<ITableReservationService, TableReservationService>();
}