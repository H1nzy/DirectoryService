namespace DirectoryService.Application;
using DirectoryService.Domain;
public interface IDepartmentLocationRepository
{
    Task<bool> ExistsAsync(Guid departmentId, Guid locationId, CancellationToken ct = default);
    Task<Guid> AddAsync(DepartmentLocation link, CancellationToken ct = default);

    Task DeleteAsync(Guid departmentId, Guid locationId, CancellationToken ct = default);
}