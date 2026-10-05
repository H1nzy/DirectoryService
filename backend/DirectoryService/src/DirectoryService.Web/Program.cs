using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using DirectoryService.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddOpenApi();

builder.Services.AddControllers();

WebApplication app = builder.Build();


app.MapGet("/", () => "DirectoryService is running!");

app.MapGet("/health", () => Results.Ok(new
{
    status = "healthy",
    timestamp = DateTimeOffset.UtcNow
}));


app.MapControllers();

if (!app.Environment.IsProduction())
{
    app.MapOpenApi();              // /openapi/v1.json
    app.MapScalarApiReference();   // /scalar/v1
}
await app.RunAsync();