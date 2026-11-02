using DirectoryService.Application;
using DirectoryService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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

    public async Task<bool> ExistsAsync(Guid departmentId, Guid locationId, CancellationToken ct = default)
    {
        return await _dbContext.DepartmentLocations.AnyAsync(x => x.DepartmentId == departmentId && x.LocationId == locationId, ct);
    }
    public async Task<Guid> AddAsync(DepartmentLocation link, CancellationToken ct = default)
    {
        try
        {
            await _dbContext.DepartmentLocations.AddAsync(link, ct);
            await _dbContext.SaveChangesAsync(ct);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Ошибка при сохранении подразделения локации {DepartmentLocationId} через EF Core", link.Id);
            throw new InvalidOperationException($"Не удалось сохранить подразделение локации'{link.Id}' в базе данных через EF Core.", ex);
        }
        return link.Id;
    }

    public async Task DeleteAsync(Guid departmentId, Guid locationId, CancellationToken ct = default)
    {
        await _dbContext.DepartmentLocations.Where(x => x.DepartmentId == departmentId && x.LocationId == locationId).ExecuteDeleteAsync(ct);
    }
}