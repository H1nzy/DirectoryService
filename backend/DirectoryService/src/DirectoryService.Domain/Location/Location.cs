using CSharpFunctionalExtensions;
using Shared;

namespace DirectoryService.Domain;

public class Location
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Address { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Location(string name, string address)
    {
        Name = name;
        Address = address;

        Id = Guid.CreateVersion7();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    private Location(Guid id, string name, string address, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        Name = name;
        Address = address;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Result<Location, Failure> Create(string? name, string? address)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.Validation(
                "location.name.is.empty",
                "Имя локации не может быть пустым",
                nameof(Name)).ToFailure();

        if (string.IsNullOrWhiteSpace(address))
            return Errors.Validation(
                "location.address.is.empty",
                "Адрес локации не может быть пустым",
                nameof(Address)).ToFailure();

        return Result.Success<Location, Failure>(new Location(name, address));
    }

    public UnitResult<Failure> Update(string? name, string? address)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.Validation(
                "location.name.is.empty",
                "Имя локации не может быть пустым",
                nameof(Name)).ToFailure();

        if (string.IsNullOrWhiteSpace(address))
            return Errors.Validation(
                "location.address.is.empty",
                "Адрес локации не может быть пустым",
                nameof(Address)).ToFailure();

        Name = name;
        Address = address;
        UpdatedAt = DateTime.UtcNow;

        return UnitResult.Success<Failure>();
    }
}