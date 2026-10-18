using System.ComponentModel;
using DirectoryService.Domain;
namespace DirectoryService.Application;
public interface ILocationRepository
{
    Task<Guid> AddAsync(Location location);
    Task<bool> CheckName(string name);

}