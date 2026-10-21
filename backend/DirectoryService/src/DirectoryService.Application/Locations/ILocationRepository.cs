using System.ComponentModel;
using DirectoryService.Domain;
namespace DirectoryService.Application;
public interface ILocationRepository
{
    Task<Guid> AddAsync(Location location, CancellationToken ct = default);
    Task<bool> CheckName(string name, CancellationToken ct = default);

}