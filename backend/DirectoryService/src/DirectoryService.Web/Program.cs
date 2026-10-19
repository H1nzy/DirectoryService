using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using DirectoryService.Infrastructure;
using DirectoryService.Application;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddOpenApi();

builder.Services.AddControllers();
builder.Services.AddScoped<CreateLocationUseCase>();
builder.Services.AddScoped<CreateLocationValidator>();

string repositoryType = "EF";
if (repositoryType == "EF")
{
    builder.Services.AddScoped<ILocationRepository, EFLocationsRepository>();
}
else if (repositoryType == "Dapper")
{
    builder.Services.AddScoped<ILocationRepository, DapperLocationRepository>();
}


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