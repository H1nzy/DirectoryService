namespace DirectoryService.Domain;

public class Department
{
    public Guid Id { get; }
    public string Name { get; private set; } = default!;
    public DepartmentSlug Slug { get; private set; } = default!;
    public string Path { get; private set; } = default!;
    public Guid? ParentId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    
    private Department(string name, DepartmentSlug slug, Guid? parentid, string path)
    {
        Name = name;
        Slug = slug;
        ParentId = parentid;
        Path = path;

        Id = Guid.CreateVersion7();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    private Department()
    {
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

    public static Department Create(string name, DepartmentSlug slug, Guid? parentId, string? parentPath)
    {
        if (slug == null)
        {
            throw new ArgumentException("Slug Обязателен для заполнения", nameof(slug));
        }
        
        if (parentId != null && string.IsNullOrWhiteSpace(parentPath))
        {
            throw new ArgumentException("Путь родительского подразделения обязателен", nameof(parentPath));
        }
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Имя не может быть пустым", nameof(name));
        }

        string finalPath;

        if (parentId == null) 
        {
            finalPath = slug.Value; 
        }
        else 
        {
            finalPath = $"{parentPath}/{slug.Value}";
        }

        Department newDepartment = new Department(name, slug, parentId, finalPath);

        return newDepartment;
    }
    
    public static Department Restore(Guid id, string name, DepartmentSlug slug, string path, Guid? parentId, DateTime createdAt, DateTime updatedAt)
    {
        return new Department(id, name, slug, path, parentId, createdAt, updatedAt);
    }
}