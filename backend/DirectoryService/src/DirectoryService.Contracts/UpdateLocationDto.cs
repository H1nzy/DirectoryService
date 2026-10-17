namespace DirectoryService.Contracts;
public record UpdateLocationDto(
    string Name,
    AddressDto Address
);