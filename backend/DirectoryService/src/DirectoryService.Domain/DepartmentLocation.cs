namespace DirectoryService.Domain;
public class DepartmentLocation
{
     public Guid Id {get;}
     public Guid DepartmentId {get; private set;}
     public Guid LocationId {get; private set;}
     public bool IsPrimary {get; private set;}

     private DepartmentLocation(Guid departmentid, Guid locationid, bool isprimary)
    {
        DepartmentId = departmentid;
        LocationId = locationid;
        IsPrimary = isprimary;

        Id = Guid.CreateVersion7();
    }

    private DepartmentLocation(){}

    public static DepartmentLocation Create(Guid departmentid, Guid locationid, bool isprimary)
    {
        if(departmentid == Guid.Empty)
        {
            throw new ArgumentException("Id подразделения не может быть пустым", nameof(departmentid));
        }
        if(locationid== Guid.Empty)
        {
            throw new ArgumentException("Id места не может быть пустым", nameof(locationid));
        }
        
        DepartmentLocation departmentLocation = new DepartmentLocation(departmentid, locationid, isprimary);
        return departmentLocation;
    }
}
