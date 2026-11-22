using DirectoryService.Application;
using DirectoryService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared;
using CSharpFunctionalExtensions;

namespace DirectoryService.Infrastructure;

public class EFLocationsRepository : ILocationRepository
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<EFLocationsRepository> _logger;

    public EFLocationsRepository(AppDbContext appDbContext, ILogger<EFLocationsRepository> logger)
    {
        _dbContext = appDbContext;
        _logger = logger;
    }

    public async Task<Result<Guid, Failure>> AddAsync(Location location, CancellationToken ct = default)
    {
        try
        {
            await _dbContext.Locations.AddAsync(location, ct);
            await _dbContext.SaveChangesAsync(ct);
            return Result.Success<Guid, Failure>(location.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при сохранении локации {LocationId} через EF Core", location.Id);
            return Errors.Failure(null, "Не удалось сохранить локацию").ToFailure();
        }
    }

    public async Task<Result<Location, Failure>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        try
        {
            var location = await _dbContext.Locations.FirstOrDefaultAsync(x => x.Id == id, ct);
            if (location is null)
                return Errors.NotFound(null, $"Локация {id} не найдена").ToFailure();
            return Result.Success<Location, Failure>(location);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка чтения локации {LocationId}", id);
            return Errors.Failure(null, "Не удалось прочитать локацию").ToFailure();
        }
    }

    public async Task<Result<bool, Failure>> CheckName(string name, CancellationToken ct = default)
    {
        try
        {
            var exists = await _dbContext.Locations.AnyAsync(x => x.Name == name, ct);
            return Result.Success<bool, Failure>(exists);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка проверки имени локации {Name}", name);
            return Errors.Failure(null, "Не удалось проверить имя локации").ToFailure();
        }
    }

    public async Task<Result<bool, Failure>> ExistsByNameExceptIdAsync(string name, Guid excludeId, CancellationToken ct = default)
    {
        try
        {
            var exists = await _dbContext.Locations.AnyAsync(x => x.Name == name && x.Id != excludeId, ct);
            return Result.Success<bool, Failure>(exists);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка проверки имени локации {Name} (excludeId={ExcludeId})", name, excludeId);
            return Errors.Failure(null, "Не удалось проверить имя локации").ToFailure();
        }
    }

    public async Task<Result<Guid, Failure>> UpdateAsync(Location location, CancellationToken ct = default)
    {
        try
        {
            _dbContext.Locations.Update(location);
            await _dbContext.SaveChangesAsync(ct);
            return Result.Success<Guid, Failure>(location.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка обновления локации {LocationId} через EF Core", location.Id);
            return Errors.Failure(null, "Не удалось обновить локацию").ToFailure();
        }
    }
}