namespace DirectoryService.Application;
using Shared;

public class DepartmentNotFoundRequestException : NotFoundRequestException
{
    public DepartmentNotFoundRequestException(Guid id, string name) : base([DepartmentErrors.Departments.NotFoundRequestException(id, name)])
    {
        
    }
}

public class ParentDepartmentNotFoundRequestException : NotFoundRequestException
{
    public ParentDepartmentNotFoundRequestException(Guid id)
        : base([DepartmentErrors.Departments.ParentNotFound(id)]) { }
}

public class LocationsNotFoundRequestException : NotFoundRequestException
{
    public LocationsNotFoundRequestException(IReadOnlyCollection<Guid> ids)
        : base([DepartmentErrors.Departments.LocationsNotFound(ids)]) { }
}