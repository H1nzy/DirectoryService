using System.Data;
using DirectoryService.Application;
using DirectoryService.Domain;
using Dapper;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Infrastructure;

public class DapperDepartmentRepository : IDepartmentRepository
{
    private readonly IDbConnection _connection;
    private readonly ILogger<DapperDepartmentRepository> _logger;

    public DapperDepartmentRepository(IDbConnection connection, ILogger<DapperDepartmentRepository> logger)
    {
        _connection = connection;
        _logger = logger;
    }

    public async Task<Department?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        // 1. Читаем в промежуточный класс, где нет Value Objects
        const string sql = "SELECT id, name, slug, path, parent_id, created_at, updated_at FROM departments WHERE id = @Id LIMIT 1;";

        var row = await _connection.QueryFirstOrDefaultAsync<DepartmentDbRow>(
            new CommandDefinition(sql, new { Id = id }, cancellationToken: ct));

        if (row == null) return null;

        return Department.Restore(
    row.Id,
    row.Name,
    DepartmentSlug.Create(row.Slug),
    row.Path,
    row.ParentId,
    row.CreatedAt,
    row.UpdatedAt);
    }

    public async Task<bool> Check(IReadOnlyCollection<Guid> locationIds, CancellationToken ct = default)
    {
        const string sql = "SELECT COUNT(*) FROM locations WHERE id = ANY(@Ids);";
        var result = await _connection.ExecuteScalarAsync<int>(new CommandDefinition(sql, new { Ids = locationIds.ToArray() }, cancellationToken: ct));

        return result == locationIds.Count;
    }

    public async Task SaveAsync(Department department, IReadOnlyCollection<DepartmentLocation> links, CancellationToken ct = default)
    {
        if (_connection.State == ConnectionState.Closed)
        {
            _connection.Open();
        }

        using var transaction = _connection.BeginTransaction();
        const string sqlDepartment = "INSERT INTO departments (id, name, slug, path, parent_id, created_at, updated_at) VALUES (@Id, @Name, @Slug, @Path, @ParentId, @CreatedAt, @UpdatedAt);";
        const string sqlLink = "INSERT INTO departments_locations (id, department_id, location_id, is_primary) VALUES (@Id, @DepartmentId, @LocationId, @IsPrimary);";

        try
        {
            await _connection.ExecuteAsync(new CommandDefinition(sqlDepartment,
            new
            {
                department.Id,
                department.Name,
                Slug = department.Slug.Value,
                department.Path,
                department.ParentId,
                department.CreatedAt,
                department.UpdatedAt
            }, transaction: transaction, cancellationToken: ct));

            await _connection.ExecuteAsync(new CommandDefinition(
                sqlLink,
                links,
                transaction: transaction,
                cancellationToken: ct));

            transaction.Commit();
        }
        catch (Exception ex)
        {
            transaction.Rollback();

            _logger.LogError(ex, "Ошибка при сохранении подразделения {DepartmentId} и его связей через Dapper", department.Id);

            throw new InvalidOperationException("Не удалось сохранить подразделение и его связи в базе данных через Dapper.", ex);
        }
    }
}

public class DepartmentDbRow
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Path { get; set; } = null!;
    public Guid? ParentId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}