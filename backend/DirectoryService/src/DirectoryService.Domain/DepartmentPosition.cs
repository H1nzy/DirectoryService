namespace DirectoryService.Domain;
public class DepartmentPosition
{
    public Guid Id {get;}
    public Guid DepartmentId {get; private set;}
    public Guid PositionId {get; private set;}

    private DepartmentPosition(Guid departmentid, Guid positionid)
    {
        DepartmentId = departmentid;
        PositionId = positionid;
        Id = Guid.CreateVersion7();
    }

    private DepartmentPosition(){}

    public static DepartmentPosition Create(Guid departmentid, Guid positionid)
    {
        if(departmentid == Guid.Empty)
        {
            throw new ArgumentException("Id подразделения не может быть пустым", nameof(departmentid));
        }
        if(positionid== Guid.Empty)
        {
            throw new ArgumentException("Id позиции не может быть пустым", nameof(positionid));
        }
        
        DepartmentPosition departmentPosition = new DepartmentPosition(departmentid, positionid);
        return departmentPosition;
    }    
}