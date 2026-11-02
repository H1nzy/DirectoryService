using DirectoryService.Domain;
namespace DirectoryService.Application;
public interface ILocationRepository
{
    Task<Location?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Guid> AddAsync(Location location, CancellationToken ct = default);
    Task<bool> CheckName(string name, CancellationToken ct = default);
    Task<bool> ExistsByNameExceptIdAsync(string name, Guid excludeId, CancellationToken ct = default);
    Task<Guid> UpdateAsync(Location location, CancellationToken ct = default);

}