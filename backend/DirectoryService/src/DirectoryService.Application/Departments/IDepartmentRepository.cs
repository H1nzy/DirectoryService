namespace DirectoryService.Application;
using DirectoryService.Domain;
public interface IDepartmentRepository
{
    Task<Department?> GetByIdAsync(Guid id, CancellationToken ct = default);
Task<bool> AllLocationsExistAsync(IReadOnlyCollection<Guid> locationIds, CancellationToken ct = default);
    Task SaveAsync(Department department, IReadOnlyCollection<DepartmentLocation> links, CancellationToken ct = default);
}