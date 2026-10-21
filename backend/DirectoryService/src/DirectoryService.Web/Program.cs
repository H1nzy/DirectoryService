using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using DirectoryService.Infrastructure;
using DirectoryService.Application;
using System.Data;
using Npgsql;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options
    .UseNpgsql(connectionString)
    .UseSnakeCaseNamingConvention());
builder.Services.AddOpenApi();

builder.Services.AddControllers();
builder.Services.AddScoped<CreateLocationUseCase>();
builder.Services.AddScoped<CreateLocationValidator>();
builder.Services.AddScoped<CreateDepartmentUseCase>();
builder.Services.AddScoped<CreateDepartmentValidator>();

string repositoryType = "EF";
if (repositoryType == "EF")
{
    builder.Services.AddScoped<ILocationRepository, EFLocationsRepository>();
    builder.Services.AddScoped<IDepartmentRepository, EFDepartmentsRepository>();
}
else if (repositoryType == "Dapper")
{
    builder.Services.AddScoped<IDbConnection>(sp => new NpgsqlConnection(connectionString));
    builder.Services.AddScoped<ILocationRepository, DapperLocationRepository>();
    builder.Services.AddScoped<IDepartmentRepository, DapperDepartmentRepository>();
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