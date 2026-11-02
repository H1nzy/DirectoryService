namespace DirectoryService.Application;
using DirectoryService.Domain;  
using DirectoryService.Contracts;
using FluentValidation;

public class UpdateDepartmentUseCase
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly UpdateDepartmentValidator _validator;
    public UpdateDepartmentUseCase(IDepartmentRepository departmentRepository, UpdateDepartmentValidator validator)
    {
        _departmentRepository = departmentRepository;
        _validator = validator;
    }

    public async Task<Guid> Handle(Guid id, UpdateDepartmentDto dto, CancellationToken ct)
    {
        var validator = await _validator.ValidateAsync(dto, ct);
        if (!validator.IsValid)
        {
            throw new ValidationException(validator.Errors);
        }

        var department = await _departmentRepository.GetByIdAsync(id, ct);
        if(department == null)
        {
            throw new InvalidOperationException("Подразделение не найдено.");
        }
        var slug = DepartmentSlug.Create(dto.Slug);
        department.Update(dto.Name, slug);
        await _departmentRepository.UpdateAsync(department, ct);
        return department.Id;
    }
}