namespace DirectoryService.Application;
using DirectoryService.Domain;
using Shared;
using CSharpFunctionalExtensions;
public interface IDepartmentRepository
{
    Task<Result<Department, Failure>> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Result<bool, Failure>> AllLocationsExistAsync(IReadOnlyCollection<Guid> locationIds, CancellationToken ct = default);
    Task<Result<bool, Failure>> ExistsBySlugExceptIdAsync(string slug, Guid excludeId, CancellationToken ct = default);
    Task<UnitResult<Failure>> SaveAsync(Department department, IReadOnlyCollection<DepartmentLocation> links, CancellationToken ct = default);
    Task<Result<Guid, Failure>> UpdateAsync(Department department, CancellationToken ct = default);
}