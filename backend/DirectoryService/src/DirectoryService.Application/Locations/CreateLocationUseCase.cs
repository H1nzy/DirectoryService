using DirectoryService.Contracts;
using FluentValidation;
using Shared;
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
            throw new LocationValidationException(validationResult.ToErrors());
        }

        bool isNameOccupied = await _locationRepository.CheckName(createLocationDto.Name, ct);
        if(isNameOccupied)
        {
            throw new LocationConflictRequestException(createLocationDto.Name);
        }

        var  location = DirectoryService.Domain.Location.Create(createLocationDto.Name, createLocationDto.Address.ToDisplayString());
        await _locationRepository.AddAsync(location, ct);
        return location.Id;
    }
}