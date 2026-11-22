using CSharpFunctionalExtensions;
using Shared;

namespace DirectoryService.Application;

public class UnLinkLocationUseCase
{
    private readonly IDepartmentLocationRepository _deplocrep;

    public UnLinkLocationUseCase(IDepartmentLocationRepository deplocrep)
    {
        _deplocrep = deplocrep;
    }

    public async Task<UnitResult<Failure>> Delete(Guid departmentId, Guid locationId, CancellationToken ct = default)
    {
        var existsResult = await _deplocrep.ExistsAsync(departmentId, locationId, ct);
        if (existsResult.IsFailure)
            return existsResult.Error;                      // 500, не 404

        if (!existsResult.Value)
            return DepartmentLocationErrors.LinkNotFoundRequestException(departmentId, locationId); // 404

        var deleteResult = await _deplocrep.DeleteAsync(departmentId, locationId, ct);
        if (deleteResult.IsFailure)
            return deleteResult.Error;                      // 500, а не ложный успех

        return UnitResult.Success<Failure>();
    }
}