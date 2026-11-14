namespace DirectoryService.Application;
using Shared;
public class DepartmentSlugConflictRequestException : ConflictRequestException
{
    public DepartmentSlugConflictRequestException(string slug, Guid id) : base([DepartmentErrors.Departments.SlugConflictRequestException(slug, id)])
    {
        
    }
}