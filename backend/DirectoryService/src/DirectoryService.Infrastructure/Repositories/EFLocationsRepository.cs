using DirectoryService.Application;
using DirectoryService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
    public async Task<Guid> AddAsync(Location location, CancellationToken ct = default)
    {
        await _dbContext.Locations.AddAsync(location,ct);
        try
        {
            await _dbContext.SaveChangesAsync(ct);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Ошибка при сохранении локации {LocationId} через EF Core", location.Id);
            throw new InvalidOperationException($"Не удалось сохранить локацию '{location.Name}' в базе данных через EF Core.", ex);
        }
        return location.Id;
    }
    public async Task<bool> CheckName(string name, CancellationToken ct = default)
    {
        return await _dbContext.Locations.AnyAsync(x => x.Name == name, ct);
    }
}
