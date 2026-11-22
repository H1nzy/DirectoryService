namespace DirectoryService.Application;
using DirectoryService.Domain;
using FluentValidation;
using DirectoryService.Contracts;
using Shared;
using CSharpFunctionalExtensions;

public class CreateDepartmentUseCase
{
    private readonly IDepartmentRepository _departmentrepository;
    private readonly CreateDepartmentValidator _departmentvalidator;

    public CreateDepartmentUseCase(IDepartmentRepository departmentRepository, CreateDepartmentValidator departmentvalidator)
    {
        _departmentrepository = departmentRepository;
        _departmentvalidator = departmentvalidator;
    }

    public async Task<Result<Department, Failure>> CreateAsync(CreateDepartmentDto createDepartmentDto, CancellationToken ct = default)
    {
        var validator = await _departmentvalidator.ValidateAsync(createDepartmentDto, ct);
        if (!validator.IsValid)
            return validator.ToErrors();

        string? parentPath = null;

        if (createDepartmentDto.ParentId.HasValue)
        {
            var parentDepartmentResult = await _departmentrepository.GetByIdAsync(createDepartmentDto.ParentId.Value, ct);
            if (parentDepartmentResult.IsFailure)
                return parentDepartmentResult.Error;        // 404/500 — как есть

            parentPath = parentDepartmentResult.Value.Path;
        }

        if (createDepartmentDto.LocationIds is { Count: > 0 })
        {
            var locationsExistResult = await _departmentrepository.AllLocationsExistAsync(createDepartmentDto.LocationIds, ct);
            if (locationsExistResult.IsFailure)
                return locationsExistResult.Error;          // 500 — сбой проверки

            if (!locationsExistResult.Value)
                return DepartmentErrors.Departments.DepartmentLocationsNotFoundRequestException(createDepartmentDto.LocationIds); // 404
        }

        var slugResult = DepartmentSlug.Create(createDepartmentDto.Slug);
        if (slugResult.IsFailure)
            return slugResult.Error;

        var departmentResult = Department.Create(
            createDepartmentDto.Name,
            slugResult.Value,
            createDepartmentDto.ParentId,
            parentPath);

        if (departmentResult.IsFailure)
            return departmentResult.Error;

        var newDepartment = departmentResult.Value;

        var links = new List<DepartmentLocation>();
        if (createDepartmentDto.LocationIds is { Count: > 0 })
        {
            foreach (var locationId in createDepartmentDto.LocationIds)
            {
                var linkResult = DepartmentLocation.Create(newDepartment.Id, locationId, false);
                if (linkResult.IsFailure)
                    return linkResult.Error;

                links.Add(linkResult.Value);
            }
        }

        var saveResult = await _departmentrepository.SaveAsync(newDepartment, links, ct);
        if (saveResult.IsFailure)
            return saveResult.Error;

        return newDepartment;
    }
}