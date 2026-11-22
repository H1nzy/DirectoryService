using DirectoryService.Application;
using DirectoryService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared;
using CSharpFunctionalExtensions;

namespace DirectoryService.Infrastructure;

public class EFDepartmentLocationRepository : IDepartmentLocationRepository
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<EFDepartmentLocationRepository> _logger;

    public EFDepartmentLocationRepository(AppDbContext appDbContext, ILogger<EFDepartmentLocationRepository> logger)
    {
        _dbContext = appDbContext;
        _logger = logger;
    }

    public async Task<Result<bool, Failure>> ExistsAsync(Guid departmentId, Guid locationId, CancellationToken ct = default)
    {
        try
        {
            var exists = await _dbContext.DepartmentLocations
                .AnyAsync(x => x.DepartmentId == departmentId && x.LocationId == locationId, ct);
            return Result.Success<bool, Failure>(exists);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка проверки связи {DepartmentId}/{LocationId}", departmentId, locationId);
            return Errors.Failure(null, "Не удалось проверить связь").ToFailure();
        }
    }

    public async Task<Result<Guid, Failure>> AddAsync(DepartmentLocation link, CancellationToken ct = default)
    {
        try
        {
            await _dbContext.DepartmentLocations.AddAsync(link, ct);
            await _dbContext.SaveChangesAsync(ct);
            return Result.Success<Guid, Failure>(link.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при сохранении связи подразделения с локацией {DepartmentLocationId}", link.Id);
            return Errors.Failure(null, "Не удалось сохранить связь подразделения с локацией").ToFailure();
        }
    }

    public async Task<Result<bool, Failure>> DeleteAsync(Guid departmentId, Guid locationId, CancellationToken ct = default)
    {
        try
        {
            var deleted = await _dbContext.DepartmentLocations
                .Where(x => x.DepartmentId == departmentId && x.LocationId == locationId)
                .ExecuteDeleteAsync(ct);

            return Result.Success<bool, Failure>(deleted > 0);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка удаления связи {DepartmentId}/{LocationId}", departmentId, locationId);
            return Errors.Failure(null, "Не удалось удалить связь").ToFailure();
        }
    }
}