namespace DirectoryService.Application;
using DirectoryService.Domain;
using FluentValidation;
using DirectoryService.Contracts;

public class CreateDepartmentUseCase
{
    private readonly IDepartmentRepository _departmentrepository;
    private readonly CreateDepartmentValidator _departmentvalidator;

    public CreateDepartmentUseCase(IDepartmentRepository departmentRepository, CreateDepartmentValidator departmentvalidator)
    {
        _departmentrepository = departmentRepository;
        _departmentvalidator = departmentvalidator;
    }

    public async Task<Department> Create(CreateDepartmentDto createDepartmentDto, CancellationToken ct = default)
    {
        var validator = await _departmentvalidator.ValidateAsync(createDepartmentDto, ct);
        if (!validator.IsValid)
        {
            throw new ValidationException(validator.Errors);
        }

        string? parentPath = null;

        if (createDepartmentDto.ParentId.HasValue)
        {
            var parentDepartment = await _departmentrepository.GetByIdAsync(createDepartmentDto.ParentId.Value, ct);

            if (parentDepartment == null)
            {
                throw new InvalidOperationException($"Родительское подразделение с ID {createDepartmentDto.ParentId.Value} не найдено.");
            }

            parentPath = parentDepartment.Path;
        }

        if (createDepartmentDto.LocationIds != null && createDepartmentDto.LocationIds.Count > 0)
        {
            bool allExist = await _departmentrepository.AllLocationsExistAsync(createDepartmentDto.LocationIds, ct);
            if (!allExist)
            {
                throw new InvalidOperationException("Одна или несколько указанных локаций не существуют в системе.");
            }

        }
        var departmentSlug = DepartmentSlug.Create(createDepartmentDto.Slug);
        var newDepartment = Department.Create(createDepartmentDto.Name, departmentSlug, createDepartmentDto.ParentId, parentPath);

        IReadOnlyCollection<DepartmentLocation> links = Array.Empty<DepartmentLocation>();
        if (createDepartmentDto.LocationIds != null && createDepartmentDto.LocationIds.Count > 0)
        {
            links = createDepartmentDto.LocationIds
        .Select(locationId => DepartmentLocation.Create(newDepartment.Id, locationId, false))
        .ToList();
        }
        await _departmentrepository.SaveAsync(newDepartment, links, ct);
        return newDepartment;

    }
}
