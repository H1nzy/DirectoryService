namespace DirectoryService.Contracts;

public record UpdateDepartmentDto(
    string Name,
    string Slug,
    Guid? ParentId
);