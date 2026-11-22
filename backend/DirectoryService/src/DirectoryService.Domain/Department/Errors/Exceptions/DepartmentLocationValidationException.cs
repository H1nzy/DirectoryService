namespace DirectoryService.Domain;
public class DepartmentLocationValidationException : BadException
{
    public DepartmentLocationValidationException(Guid locationId): base(DepartmentErrors.DepartmentLocation.ValidationException(locationId))
    {
        
    }
}

public class DepartmentLocationIdValidationException : BadException
{
    public DepartmentLocationIdValidationException(Guid departmentId): base(DepartmentErrors.DepartmentLocationId.ValidationException(departmentId))
    {
        
    }
}