using DirectoryService.Domain;
using Shared;
using CSharpFunctionalExtensions;
namespace DirectoryService.Application;
public interface ILocationRepository
{
    Task<Result<Location, Failure>> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Result<Guid, Failure>> AddAsync(Location location, CancellationToken ct = default);
    Task<Result<bool, Failure>> CheckName(string name, CancellationToken ct = default);
    Task<Result<bool, Failure>> ExistsByNameExceptIdAsync(string name, Guid excludeId, CancellationToken ct = default);
    Task<Result<Guid, Failure>> UpdateAsync(Location location, CancellationToken ct = default);

}