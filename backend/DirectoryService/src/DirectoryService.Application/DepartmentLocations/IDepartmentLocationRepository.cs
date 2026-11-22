namespace DirectoryService.Application;
using DirectoryService.Domain;
using Shared;
using CSharpFunctionalExtensions;
public interface IDepartmentLocationRepository
{
    Task<Result<bool, Failure>> ExistsAsync(Guid departmentId, Guid locationId, CancellationToken ct = default);
    Task<Result<Guid, Failure>> AddAsync(DepartmentLocation link, CancellationToken ct = default);
    Task<Result<bool, Failure>> DeleteAsync(Guid departmentId, Guid locationId, CancellationToken ct = default);
}