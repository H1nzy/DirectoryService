using DirectoryService.Application;
using DirectoryService.Contracts;
using Shared;
namespace DirectoryService.Application;
public class LocationConflictRequestException : ConflictRequestException
{
    public LocationConflictRequestException(string name) : base([LocationErrors.Locations.ConflictRequestException(name)])
    {
        
    }
}