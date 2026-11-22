namespace DirectoryService.Domain;
public class LocationNameValidationException : BadException
{
    public LocationNameValidationException(string address): base(LocationErrors.LocationName.ValidationException(address))
    {
        
    }
}

public class LocationAddressValidationException : BadException
{
    public LocationAddressValidationException(string name): base(LocationErrors.LocationAddress.ValidationException(name))
    {
        
    }
}