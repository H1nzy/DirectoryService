using CSharpFunctionalExtensions;
using Shared;

namespace DirectoryService.Domain;

public class Department
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public DepartmentSlug Slug { get; private set; }
    public string Path { get; private set; }
    public Guid? ParentId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Department(string name, DepartmentSlug slug, Guid? parentId, string path)
    {
        Name = name;
        Slug = slug;
        ParentId = parentId;
        Path = path;

        Id = Guid.CreateVersion7();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    private Department(Guid id, string name, DepartmentSlug slug, string path, Guid? parentId, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        Name = name;
        Slug = slug;
        Path = path;
        ParentId = parentId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Result<Department, Failure> Create(string? name, DepartmentSlug? slug, Guid? parentId, string? parentPath)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.Validation(
                "department.name.is.empty",
                "Имя подразделения не может быть пустым",
                nameof(Name)).ToFailure();

        if (slug is null)
            return Errors.Validation(
                "department.slug.is.empty",
                "Slug подразделения не может быть пустым",
                nameof(Slug)).ToFailure();

        if (parentId is not null && string.IsNullOrWhiteSpace(parentPath))
            return Errors.Validation(
                "department.parent.path.is.empty",
                "Путь родительского подразделения не может быть пустым",
                nameof(ParentId)).ToFailure();

        string finalPath = parentId is null
            ? slug.Value
            : $"{parentPath}/{slug.Value}";

        return Result.Success<Department, Failure>(
            new Department(name, slug, parentId, finalPath));
    }

    public UnitResult<Failure> Update(string? name, DepartmentSlug? slug)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.Validation(
                "department.name.is.empty",
                "Имя подразделения не может быть пустым",
                nameof(Name)).ToFailure();

        if (slug is null)
            return Errors.Validation(
                "department.slug.is.empty",
                "Slug подразделения не может быть пустым",
                nameof(Slug)).ToFailure();

        Name = name;
        Slug = slug;
        UpdatedAt = DateTime.UtcNow;

        return Result.Success<Failure>();
    }
}