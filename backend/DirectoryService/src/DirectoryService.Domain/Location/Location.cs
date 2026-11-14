namespace DirectoryService.Domain;
public class Location
{
     public Guid Id {get; private set;}
     public string Name{get; private set;}
     public string Address {get; private set;}

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
    private Location(Guid id, string name, string address, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        Name = name;
        Address = address;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Location Create(string name, string address)
    {
        if (string.IsNullOrWhiteSpace(name)) {
        throw new LocationNameValidationException(address);
        }
        if (string.IsNullOrWhiteSpace(address)) {
        throw new LocationAddressValidationException(name);
        }
        Location location = new Location(name, address);
        return location;
    }

    public void Update(string name, string address)
    {
        if (string.IsNullOrWhiteSpace(name)) {
        throw new LocationNameValidationException(address);
        }
        if (string.IsNullOrWhiteSpace(address)) {
        throw new LocationAddressValidationException(name);
        }
        this.Name = name;
        this.Address = address;
        this.UpdatedAt = DateTime.UtcNow;
    }

}