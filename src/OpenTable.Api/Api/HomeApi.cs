namespace OpenTable.Api.Api;

internal static class HomeApi
{
    public static WebApplication UseHomeApi(this WebApplication app)
    {
        app.MapGet("api-name", (IOptions<AppOptions> options) => Results.Ok(options.Value.Name));
        app.MapGet("health-check", () => Results.Ok("Health"));

        return app;
    }
}