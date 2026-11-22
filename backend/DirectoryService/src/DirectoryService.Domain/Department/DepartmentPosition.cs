using CSharpFunctionalExtensions;
using Shared;

namespace DirectoryService.Domain;

public class DepartmentPosition
{
    public Guid Id { get; }
    public Guid DepartmentId { get; private set; }
    public Guid PositionId { get; private set; }

    private DepartmentPosition(Guid departmentId, Guid positionId)
    {
        DepartmentId = departmentId;
        PositionId = positionId;
        Id = Guid.CreateVersion7();
    }

    private DepartmentPosition() { }

    public static Result<DepartmentPosition, Failure> Create(Guid departmentId, Guid positionId)
    {
        if (departmentId == Guid.Empty)
            return Errors.Validation(
                "department.position.department.id.is.empty",
                "Id подразделения не может быть пустым",
                nameof(DepartmentId)).ToFailure();

        if (positionId == Guid.Empty)
            return Errors.Validation(
                "department.position.position.id.is.empty",
                "Id должности не может быть пустым",
                nameof(PositionId)).ToFailure();

        return Result.Success<DepartmentPosition, Failure>(
            new DepartmentPosition(departmentId, positionId));
    }
}