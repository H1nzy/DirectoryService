namespace DirectoryService.Domain;
public class DepartmentPositionValidationException : BadException
{
    public DepartmentPositionValidationException(Guid positionId): base([DepartmentErrors.DepartmentPosition.ValidationException(positionId)])
    {
        
    }
}

public class DepartmentPositionIdValidationException : BadException
{
    public DepartmentPositionIdValidationException(Guid departmentId): base([DepartmentErrors.DepartmentPositionId.ValidationException(departmentId)])
    {
        
    }
}