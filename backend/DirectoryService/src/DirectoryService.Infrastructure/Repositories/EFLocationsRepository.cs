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
    public async Task<Guid> AddAsync(Location location)
    {
        await _dbContext.Locations.AddAsync(location);
        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Ошибка при сохранении локации {LocationId} через EF Core", location.Id);
            throw new InvalidOperationException($"Не удалось сохранить локацию '{location.Name}' в базе данных через EF Core.", ex);
        }
        return location.Id;
    }

    public async Task<bool> CheckName(string name)
    {
public async Task<bool> CheckName(string name, CancellationToken ct = default)
    }
}