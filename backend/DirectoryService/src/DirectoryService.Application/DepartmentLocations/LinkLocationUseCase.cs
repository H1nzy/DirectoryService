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
            throw new InvalidOperationException("Подразделение не найдено");
        }

        var location = await _locrep.GetByIdAsync(locationId, ct);
        if (location == null)
        {
            throw new InvalidOperationException("Локация не найдена");
        }

        var exists = await _deplocrep.ExistsAsync(departmentId, locationId, ct);
        if (exists)
        {
            throw new InvalidOperationException("Связь уже существует");
        }

        var link = DepartmentLocation.Create(departmentId, locationId, false);
        return await _deplocrep.AddAsync(link, ct);

    }
}