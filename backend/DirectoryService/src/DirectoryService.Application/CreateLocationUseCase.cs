using DirectoryService.Contracts;
using DirectoryService.Domain;
using FluentValidation;
namespace DirectoryService.Application;
 public class CreateLocationUseCase
{
    private readonly ILocationRepository _locationRepository;
    private readonly CreateLocationValidator _validator;
    public CreateLocationUseCase(ILocationRepository locationRepository, CreateLocationValidator validator)
    {
        _locationRepository = locationRepository;
        _validator = validator;
    }
    public async Task<Guid> Create(CreateLocationDto createLocationDto,CancellationToken ct = default)
    {
        var validationResult = await _validator.ValidateAsync(createLocationDto, ct);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        bool isNameOccupied = await _locationRepository.CheckName(createLocationDto.Name, ct);
        if(isNameOccupied)
        {
            throw new InvalidOperationException($"Location with name '{createLocationDto.Name}' already exists.");
        }

        var  location = DirectoryService.Domain.Location.Create(createLocationDto.Name, createLocationDto.Address.City + ", " + createLocationDto.Address.Street + ", " + createLocationDto.Address.House + (createLocationDto.Address.Apartment != null ? ", " + createLocationDto.Address.Apartment : ""));
        await _locationRepository.AddAsync(location, ct);
        return location.Id;
    }
}