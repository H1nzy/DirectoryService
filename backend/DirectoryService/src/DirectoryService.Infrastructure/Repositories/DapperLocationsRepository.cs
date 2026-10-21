using System.Data;
using DirectoryService.Application;
using DirectoryService.Domain;
using Dapper;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Infrastructure;
public class DapperLocationRepository : ILocationRepository
{
    private readonly IDbConnection _connection;
    private readonly ILogger<DapperLocationRepository> _logger;
    public DapperLocationRepository(IDbConnection connection, ILogger<DapperLocationRepository> logger)
    {
        _connection = connection;
        _logger = logger;
    }
    public async Task<Guid> AddAsync(Location location, CancellationToken ct = default)
    {
        const string sql = @"
        INSERT INTO locations (id, name, address, created_at, updated_at) 
        VALUES (@Id, @Name, @Address, @CreatedAt, @UpdatedAt);";
        try
        {
            var command = new CommandDefinition(sql, location, cancellationToken: ct);
            await _connection.ExecuteAsync(command);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Ошибка при сохранении локации {LocationId} через Dapper", location.Id);
            throw new InvalidOperationException($"Не удалось выполнить SQL-команду вставки для локации '{location.Name}' через Dapper.", ex);
        }
        return location.Id;
    }

    public async Task<bool> CheckName(string name, CancellationToken ct = default)
    {
        const string sql = "SELECT EXISTS(SELECT 1 FROM locations WHERE name = @Name);";
        var command = new CommandDefinition(sql, new {Name = name}, cancellationToken: ct);
        return await _connection.QueryFirstOrDefaultAsync<bool>(command);
    }
}