namespace DirectoryService.Application;
using DirectoryService.Domain;
using DirectoryService.Contracts;
using FluentValidation;
using Shared;
using CSharpFunctionalExtensions;

public class UpdateDepartmentUseCase
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly UpdateDepartmentValidator _validator;

    public UpdateDepartmentUseCase(IDepartmentRepository departmentRepository, UpdateDepartmentValidator validator)
    {
        _departmentRepository = departmentRepository;
        _validator = validator;
    }

    public async Task<Result<Guid, Failure>> Handle(Guid id, UpdateDepartmentDto dto, CancellationToken ct)
    {
        var validator = await _validator.ValidateAsync(dto, ct);
        if (!validator.IsValid)
            return validator.ToErrors();

        var departmentResult = await _departmentRepository.GetByIdAsync(id, ct);
        if (departmentResult.IsFailure)
            return departmentResult.Error;

        var isSlugOccupied = await _departmentRepository.ExistsBySlugExceptIdAsync(dto.Slug, id, ct);
        if (isSlugOccupied.IsFailure)
            return isSlugOccupied.Error;

        if (isSlugOccupied.Value)
            return DepartmentErrors.Departments.DepartmentSlugConflictRequestException(dto.Slug, id);

        var slugResult = DepartmentSlug.Create(dto.Slug);
        if (slugResult.IsFailure)
            return slugResult.Error;                     

        var updateResult = departmentResult.Value.Update(dto.Name, slugResult.Value);
        if (updateResult.IsFailure)
            return updateResult.Error;                    

        var saveResult = await _departmentRepository.UpdateAsync(departmentResult.Value, ct);
        if (saveResult.IsFailure)
            return saveResult.Error;                    

        return departmentResult.Value.Id;
    }
}