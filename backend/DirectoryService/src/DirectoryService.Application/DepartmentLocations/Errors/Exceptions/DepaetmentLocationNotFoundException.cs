namespace DirectoryService.Application;
using Shared;

public class DepartmentLocationNotFoundRequestException : NotFoundRequestException
{
    public DepartmentLocationNotFoundRequestException(Guid departmentId) : base([DepartmentLocationErrors.DepartmentLocationNotFound(departmentId)])
    {
        
    }
}

public class LocationDepartmentNotFoundRequestException : NotFoundRequestException
{
    public LocationDepartmentNotFoundRequestException(Guid locationId) : base([DepartmentLocationErrors.LocationDepartmentNotFound(locationId)])
    {
        
    }
}

public class ExistsConflictRequestException : ConflictRequestException
{
    public ExistsConflictRequestException(Guid departmentId, Guid locationId) : base([DepartmentLocationErrors.ExistsConflictRequestException(departmentId, locationId)])
    {
        
    }
}

public class LinkNotFoundRequestException : NotFoundRequestException
{
    public LinkNotFoundRequestException(Guid departmentId, Guid locationId) : base([DepartmentLocationErrors.LinkNotFoundRequestException(departmentId, locationId)])
    {
        
    }
}