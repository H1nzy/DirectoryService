namespace DirectoryService.Domain;
public class Position
{
    public Guid Id {get;}
    public string Name{get; private set;} = default!;
    public DateTime CreatedAt {get; private set;}
    public DateTime UpdatedAt {get; private set;}

    private Position(string name)
    {
        Name = name;

        Id = Guid.CreateVersion7();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    private Position(){}

    public static Position Create(string name)
    {
         if (string.IsNullOrWhiteSpace(name)) {
        throw new ArgumentException("Имя не может быть пустым", nameof(name));
        }

        Position position = new Position(name);
        return position;
    }

}