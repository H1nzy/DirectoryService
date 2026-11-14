namespace DirectoryService.Application;
using Shared;

public static partial class DepartmentErrors
{
    public static class Departments
    {
        public static Errors ConflictRequestException(string name) => Errors.Conflict(
                    "department.name.conflict",
                    $"Department with name '{name}' already exists.");
        public static Errors NotFoundRequestException(Guid id, string name) => Errors.NotFound(
                    "department.name.and.id.not.found",
                    $"Department  with name '{name}' and id '{id}' not found.");
            
        public static Errors ParentNotFound(Guid id) => Errors.NotFound(
                    "department.parent.not.found",
                    $"ParentDepartment with id '{id}' not found.");
        public static Errors SlugConflictRequestException(string slug , Guid id) => Errors.Conflict(
                    "department.slug.conflict",
                    $"Department with slug '{slug}' already exists (conflicts with updating department id '{id}').");
        public static Errors LocationsNotFound(IReadOnlyCollection<Guid> ids) => Errors.NotFound(
                    "department.locations.not.found",
                    $"Locations with ids '{string.Join(", ", ids)}' not found.");
                    
    }
}