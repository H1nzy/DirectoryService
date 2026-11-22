using CSharpFunctionalExtensions;
using Shared;

namespace DirectoryService.Domain;

public class Position
{
    public Guid Id { get; }
    public string Name { get; private set; } = default!;
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Position(string name)
    {
        Name = name;

        Id = Guid.CreateVersion7();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    private Position() { }

    public static Result<Position, Failure> Create(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.Validation(
                "position.name.is.empty",
                "Имя должности не может быть пустым",
                nameof(Name)).ToFailure();

        return Result.Success<Position, Failure>(new Position(name));
    }
}