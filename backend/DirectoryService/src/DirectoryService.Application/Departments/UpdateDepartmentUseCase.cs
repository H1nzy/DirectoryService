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
            throw new DepartmentValidationException(validator.ToErrors());
        }

        var department = await _departmentRepository.GetByIdAsync(id, ct);
        if(department == null)
        {
            throw new DepartmentNotFoundRequestException(id, dto.Name);
        }
        bool isSlugOccupied = await _departmentRepository.ExistsBySlugExceptIdAsync(dto.Slug, id, ct);
        if (isSlugOccupied)
        {
            throw new DepartmentSlugConflictRequestException(dto.Slug, id);
        }
        var slug = DepartmentSlug.Create(dto.Slug);
        department.Update(dto.Name, slug);
        await _departmentRepository.UpdateAsync(department, ct);
        return department.Id;
    }
}