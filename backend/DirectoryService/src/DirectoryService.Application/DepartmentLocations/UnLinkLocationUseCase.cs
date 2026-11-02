namespace DirectoryService.Application;
using DirectoryService.Domain;

public class UnLinkLocationUseCase
{
    private readonly IDepartmentLocationRepository _deplocrep;

    public UnLinkLocationUseCase(IDepartmentLocationRepository deplocrep)
    {
        _deplocrep = deplocrep;
    }

    public async Task Delete(Guid departmentId, Guid locationId, CancellationToken ct = default)
    {
        var exists = await _deplocrep.ExistsAsync(departmentId, locationId, ct);
        if (!exists)
        {
            throw new InvalidOperationException("Связь не найдена");
        }
        await _deplocrep.DeleteAsync(departmentId, locationId, ct);
    }
}