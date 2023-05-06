using OpenTable.Api.Api;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddCore()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.UseSerilog();

var app = builder.Build();
app.UseInfrastructure();
app.UseHomeApi();

app.Run();
