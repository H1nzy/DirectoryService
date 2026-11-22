namespace DirectoryService.Domain;
using Shared;
 public class DepartmentValidationException : BadException
{
    public DepartmentValidationException(): base(DepartmentErrors.Department.ValidationException())
    {
        
    }
}

public class SlugValidationException : BadException
{
    public SlugValidationException(): base(DepartmentErrors.Slug.ValidationException())
    {
        
    }
}

public class DepartmentParentValidationException : BadException
{
    public DepartmentParentValidationException(Guid id): base(DepartmentErrors.DepartmentParent.ValidationException(id))
    {
        
    }
}