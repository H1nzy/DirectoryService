using DirectoryService.Domain;
using Shared;
using CSharpFunctionalExtensions;

namespace DirectoryService.Application;

public class LinkLocationUseCase
{
    private readonly IDepartmentLocationRepository _deplocrep;
    private readonly IDepartmentRepository _deprep;
    private readonly ILocationRepository _locrep;

    public LinkLocationUseCase(IDepartmentLocationRepository deplocrep, IDepartmentRepository deprep, ILocationRepository locrep)
    {
        _deplocrep = deplocrep;
        _deprep = deprep;
        _locrep = locrep;
    }

    public async Task<Result<Guid, Failure>> Create(Guid departmentId, Guid locationId, CancellationToken ct = default)
    {
        var departmentResult = await _deprep.GetByIdAsync(departmentId, ct);
        if (departmentResult.IsFailure)
            return departmentResult.Error;                 // 404/500 — как есть

        var locationResult = await _locrep.GetByIdAsync(locationId, ct);
        if (locationResult.IsFailure)
            return locationResult.Error;                    // 404/500 — как есть

        var existsResult = await _deplocrep.ExistsAsync(departmentId, locationId, ct);
        if (existsResult.IsFailure)
            return existsResult.Error;                      // 500
        if (existsResult.Value)
            return DepartmentLocationErrors.ExistsConflictRequestException(departmentId, locationId); // 409

        var linkResult = DepartmentLocation.Create(departmentId, locationId, false);
        if (linkResult.IsFailure)
            return linkResult.Error;                        // 400 VALIDATION (пустые id)

        return await _deplocrep.AddAsync(linkResult.Value, ct);   // AddAsync вернёт Failure при сбое БД
    }
}