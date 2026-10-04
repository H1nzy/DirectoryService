namespace DirectoryService.Domain;
public class Location
{
     public Guid Id {get;}
     public string Name{get; private set;} = default!;
     public string Address {get; private set;} = default!;

    public DateTime CreatedAt {get; private set;}
    public DateTime UpdatedAt {get; private set;}

    private Location(string name, string address)
    {
        Name = name;
        Address = address;

        Id = Guid.CreateVersion7();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;    
    }

    private Location(){}

    public static Location Create(string name, string address)
    {
        if (string.IsNullOrWhiteSpace(name)) {
        throw new ArgumentException("Имя не может быть пустым", nameof(name));
        }
        if (string.IsNullOrWhiteSpace(address)) {
        throw new ArgumentException("Адрес не может быть пустым", nameof(address));
        }
        Location location = new Location(name, address);
        return location;
    }

}