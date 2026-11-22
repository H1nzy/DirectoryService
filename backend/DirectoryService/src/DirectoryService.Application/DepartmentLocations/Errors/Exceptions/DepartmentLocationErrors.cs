namespace DirectoryService.Application;
using Shared;

public static partial class DepartmentLocationErrors
{
    public static Failure DepartmentLocationNotFound(Guid departmentId) => Errors.NotFound(
        "department.location.not.found",
        $"Department with ID '{departmentId}' not found.");

    public static Failure LocationDepartmentNotFound(Guid locationId) => Errors.NotFound(
        "location.department.not.found",
        $"Location with ID '{locationId}' not found.");

    public static Failure ExistsConflictRequestException(Guid departmentId, Guid locationId) => Errors.Conflict(
        "exists.conflict",
        $"Link between Department with ID '{departmentId}' and Location with ID '{locationId}' already exists.");

    public static Failure LinkNotFoundRequestException(Guid departmentId, Guid locationId) => Errors.NotFound(
        "link.not.found",
        $"Link between Department with ID '{departmentId}' and Location with ID '{locationId}' not found.");
}