using System.Text.Json;
using Shared;

namespace DirectoryService.Application;
public class LocationNotFoundRequestException : NotFoundRequestException
{
    public LocationNotFoundRequestException(Guid id, string name) : base(LocationErrors.Locations.LocationNotFoundRequestException(id, name))
    {
        
    }
}