using DirectoryService.Application;
using DirectoryService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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

    public async Task<Department?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _dbContext.Departments.FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<bool> Check(IReadOnlyCollection<Guid> locationIds, CancellationToken ct = default)
    {
          int result = await _dbContext.Locations
        .Where(l => locationIds.Contains(l.Id))
        .CountAsync(ct);

        return result == locationIds.Count;
    }

    public async Task SaveAsync(Department department, IReadOnlyCollection<DepartmentLocation> links, CancellationToken ct = default)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync(ct);
        try
        {
            await _dbContext.Departments.AddAsync(department, ct);
            await _dbContext.DepartmentLocations.AddRangeAsync(links, ct);
            await _dbContext.SaveChangesAsync(ct);
            await transaction.CommitAsync(ct);

        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(ct);
            _logger.LogError(ex, "Ошибка при сохранении подразделения {DepartmentId}", department.Id);
            throw new InvalidOperationException("Не удалось сохранить подразделение.", ex);
        }
    }


}