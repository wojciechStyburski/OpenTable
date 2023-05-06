namespace OpenTable.Tests.Integration;

internal class OpenTableTestApp : WebApplicationFactory<Program>
{
    public HttpClient Client { get;}
    public OpenTableTestApp(Action<IServiceCollection> services)
    {
        Client = WithWebHostBuilder(builder =>
        {
            if (services is not null)
            {
                builder.ConfigureServices(services);
            }
            
            builder.UseEnvironment("Test");
        }).CreateClient();
    }
}