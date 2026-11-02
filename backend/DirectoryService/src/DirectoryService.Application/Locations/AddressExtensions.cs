using DirectoryService.Contracts;

namespace DirectoryService.Application;
public static class AddressExtensions 
{
    public static string ToDisplayString(this AddressDto address)
    {
        return address.City + ", " + address.Street + ", " + address.House + (!string.IsNullOrWhiteSpace(address.Apartment) ? ", " + address.Apartment : "");
    }
}