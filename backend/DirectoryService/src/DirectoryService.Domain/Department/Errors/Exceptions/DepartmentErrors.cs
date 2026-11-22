namespace DirectoryService.Domain;
using Shared;
 public static class DepartmentErrors
{
    public static class Department
    {
        public static Failure ValidationException() => Errors.Validation("department.name.is.space", $"Name of department is required.");
    }

    public static class Slug
    {
        public static Failure ValidationException() => Errors.Validation("department.slug.is.space", $"Slug of department is required.");
    }

    public static class DepartmentParent
    {
        public static Failure ValidationException(Guid id) => Errors.Validation("department.parent.required", $"Department with id {id} must have a parent department.");
    }

    public static class DepartmentLocation
    {
        public static Failure ValidationException(Guid locationId) => Errors.Validation(
                    "departmentid.location.required",
                    $"Location with id '{locationId}' should be associated with a valid department.");
    }

    public static class DepartmentLocationId
    {
        public static Failure ValidationException(Guid departmentId) => Errors.Validation(
                    "department.locationId.required",
                    $"Location id for department '{departmentId}' is required.");
        
    }

    public static class DepartmentPosition
    {
        public static Failure ValidationException(Guid positionId) => Errors.Validation(
                    "department.position.required",
                    $"Position with id '{positionId}' should be associated with a valid department.");
    }

    public static class DepartmentPositionId
    {
        public static Failure ValidationException(Guid departmentId) => Errors.Validation(
                    "department.positionId.required",
                    $"Position id for department '{departmentId}' is required.");
        
    }

    public static class DepartmentSlug
    {
        public static Failure ValidationException(string slug) => Errors.Validation(
                    "department.slug.invalid",
                    $"Slug '{slug}' is invalid for department. Correct format is 2 to 100 characters, only lowercase Latin letters, numbers, and hyphens; the string does not start or end with a hyphen.");
    }
}