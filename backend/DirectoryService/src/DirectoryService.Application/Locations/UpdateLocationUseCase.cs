namespace DirectoryService.Application;
using DirectoryService.Domain;
using FluentValidation;
using DirectoryService.Contracts;

public class UpdateLocationUseCase
{
    private readonly ILocationRepository _locationRepository;
    private readonly UpdateLocationValidator _validator;
    public UpdateLocationUseCase(ILocationRepository locationRepository, UpdateLocationValidator validator)
    {
        _locationRepository = locationRepository;
        _validator = validator;
    }

    public async Task<Guid> Handle(Guid id, UpdateLocationDto dto, CancellationToken ct)
    {
        var validationResult = await _validator.ValidateAsync(dto, ct);
        if (!validationResult.IsValid)
        {
             throw new LocationValidationException(validationResult.ToErrors());
        }

        var location = await _locationRepository.GetByIdAsync(id, ct);
        if (location == null)
        {
            throw new LocationNotFoundRequestException(id, dto.Name);
        }

        bool isNameOccupied = await _locationRepository.ExistsByNameExceptIdAsync(dto.Name, id, ct);
        if(isNameOccupied)
        {
            throw new LocationConflictRequestException(dto.Name);
        }
        location.Update(dto.Name, dto.Address.ToDisplayString());
        await _locationRepository.UpdateAsync(location, ct);
        return location.Id;
        
    }
}