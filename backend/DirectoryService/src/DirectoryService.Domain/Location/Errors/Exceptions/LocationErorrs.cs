namespace DirectoryService.Domain;
using Shared;
public static class LocationErrors
{
    public static class LocationName
    {
        public static Failure ValidationException(string address) => Errors.Validation("location.name.is.space", $"The location with the address {address} has an empty name.");
    }
    public static class LocationAddress
    {
        public static Failure ValidationException(string name) => Errors.Validation("location.address.is.space", $"The location with the name {name} has an empty address.");
    }
}