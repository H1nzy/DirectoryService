namespace DirectoryService.Domain;

public class DepartmentSlugValidationException : BadException
{
    public DepartmentSlugValidationException(string slug): base(DepartmentErrors.DepartmentSlug.ValidationException(slug))
    {
        
    }
}