using DirectoryService.Application;
using DirectoryService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared;
using CSharpFunctionalExtensions;

namespace DirectoryService.Infrastructure;

public class EFDepartmentsRepository : IDepartmentRepository
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<EFDepartmentsRepository> _logger;

    public EFDepartmentsRepository(AppDbContext appDbContext, ILogger<EFDepartmentsRepository> logger)
    {
        _dbContext = appDbContext;
        _logger = logger;
    }

    public async Task<Result<Department, Failure>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        try
        {
            var department = await _dbContext.Departments.FirstOrDefaultAsync(x => x.Id == id, ct);
            if (department is null)
                return Errors.NotFound(null, $"Подразделение {id} не найдено").ToFailure();
            return Result.Success<Department, Failure>(department);
        }
        catch (OperationCanceledException) { throw; }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка чтения подразделения {DepartmentId}", id);
            return Errors.Failure(null, "Не удалось прочитать подразделение").ToFailure();
        }
    }

    public async Task<Result<bool, Failure>> AllLocationsExistAsync(IReadOnlyCollection<Guid> locationIds, CancellationToken ct = default)
    {
        try
        {
            var result = await _dbContext.Locations
                .Where(l => locationIds.Contains(l.Id))
                .CountAsync(ct);

            return Result.Success<bool, Failure>(result == locationIds.Count);
        }
        catch (OperationCanceledException) { throw; }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка проверки локаций {LocationIds}", locationIds);
            return Errors.Failure(null, "Не удалось проверить локации").ToFailure();
        }
    }

    public async Task<UnitResult<Failure>> SaveAsync(Department department, IReadOnlyCollection<DepartmentLocation> links, CancellationToken ct = default)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(ct);
        try
        {
            await _dbContext.Departments.AddAsync(department, ct);
            await _dbContext.DepartmentLocations.AddRangeAsync(links, ct);
            await _dbContext.SaveChangesAsync(ct);
            await transaction.CommitAsync(ct);
            return UnitResult.Success<Failure>();
        }
        catch (OperationCanceledException) { throw; }
        catch (Exception ex)
        {
            try { await transaction.RollbackAsync(ct); }
            catch (Exception rollbackEx) { _logger.LogError(rollbackEx, "Ошибка отката транзакции для {DepartmentId}", department.Id); }

            _logger.LogError(ex, "Ошибка при сохранении подразделения {DepartmentId}", department.Id);
            return Errors.Failure(null, "Не удалось сохранить подразделение").ToFailure();
        }
    }

    public async Task<Result<Guid, Failure>> UpdateAsync(Department department, CancellationToken ct = default)
    {
        try
        {
            _dbContext.Departments.Update(department);
            await _dbContext.SaveChangesAsync(ct);
            return Result.Success<Guid, Failure>(department.Id);
        }
        catch (OperationCanceledException) { throw; }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка обновления подразделения {DepartmentId} через EF Core", department.Id);
            return Errors.Failure(null, "Не удалось обновить подразделение").ToFailure();
        }
    }

   public async Task<Result<bool, Failure>> ExistsBySlugExceptIdAsync(string slug, Guid excludeId, CancellationToken ct = default)
{
    try
    {
        var slugResult = DepartmentSlug.Create(slug);
        if (slugResult.IsFailure)
            return slugResult.Error;

        // Сравниваем сам объект-значение, не вызывая .Value
        var exists = await _dbContext.Departments
            .AnyAsync(x => x.Slug == slugResult.Value && x.Id != excludeId, ct);

        return Result.Success<bool, Failure>(exists);
    }
    catch (OperationCanceledException) { throw; }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Ошибка проверки slug {Slug} (excludeId={ExcludeId})", slug, excludeId);
        return Errors.Failure(null, "Не удалось проверить slug").ToFailure();
    }
}
}