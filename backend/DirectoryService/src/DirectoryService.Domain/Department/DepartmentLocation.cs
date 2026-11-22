using CSharpFunctionalExtensions;
using Shared;

namespace DirectoryService.Domain;

public class DepartmentLocation
{
    public Guid Id { get; private set; }
    public Guid DepartmentId { get; private set; }
    public Guid LocationId { get; private set; }
    public bool IsPrimary { get; private set; }

    private DepartmentLocation(Guid departmentId, Guid locationId, bool isPrimary)
    {
        DepartmentId = departmentId;
        LocationId = locationId;
        IsPrimary = isPrimary;

        Id = Guid.CreateVersion7();
    }

    private DepartmentLocation() { }

    public static Result<DepartmentLocation, Failure> Create(Guid departmentId, Guid locationId, bool isPrimary)
    {
        if (departmentId == Guid.Empty)
            return Errors.Validation(
                "department.location.department.id.is.empty",
                "Id подразделения не может быть пустым",
                nameof(DepartmentId)).ToFailure();

        if (locationId == Guid.Empty)
            return Errors.Validation(
                "department.location.location.id.is.empty",
                "Id локации не может быть пустым",
                nameof(LocationId)).ToFailure();

        return Result.Success<DepartmentLocation, Failure>(
            new DepartmentLocation(departmentId, locationId, isPrimary));
    }
}