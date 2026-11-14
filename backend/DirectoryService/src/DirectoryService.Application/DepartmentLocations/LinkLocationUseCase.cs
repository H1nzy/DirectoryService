namespace DirectoryService.Application;
using DirectoryService.Domain;

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
    public async Task<Guid> Create(Guid departmentId, Guid locationId, CancellationToken ct = default)
    {
        var department = await _deprep.GetByIdAsync(departmentId, ct);
        if (department == null)
        {
            throw new DepartmentLocationNotFoundRequestException(departmentId);
        }

        var location = await _locrep.GetByIdAsync(locationId, ct);
        if (location == null)
        {
            throw new LocationDepartmentNotFoundRequestException(locationId);
        }

        var exists = await _deplocrep.ExistsAsync(departmentId, locationId, ct);
        if (exists)
        {
            throw new ExistsConflictRequestException(departmentId, locationId);
        }

        var link = DepartmentLocation.Create(departmentId, locationId, false);
        return await _deplocrep.AddAsync(link, ct);

    }
}