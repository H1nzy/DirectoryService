namespace DirectoryService.Application;
using DirectoryService.Domain;
using FluentValidation;
using DirectoryService.Contracts;
using Shared;
using CSharpFunctionalExtensions;

public class UpdateLocationUseCase
{
    private readonly ILocationRepository _locationRepository;
    private readonly UpdateLocationValidator _validator;

    public UpdateLocationUseCase(ILocationRepository locationRepository, UpdateLocationValidator validator)
    {
        _locationRepository = locationRepository;
        _validator = validator;
    }

    public async Task<Result<Guid, Failure>> Handle(Guid id, UpdateLocationDto dto, CancellationToken ct)
    {
        var validationResult = await _validator.ValidateAsync(dto, ct);
        if (!validationResult.IsValid)
            return validationResult.ToErrors();

        var locationResult = await _locationRepository.GetByIdAsync(id, ct);
        if (locationResult.IsFailure)
            return locationResult.Error;

        var isNameOccupied = await _locationRepository.ExistsByNameExceptIdAsync(dto.Name, id, ct);
        if (isNameOccupied.IsFailure)
            return isNameOccupied.Error;

        if (isNameOccupied.Value)
            return LocationErrors.Locations.LocationConflictRequestException(dto.Name);

        var updateResult = locationResult.Value.Update(dto.Name, dto.Address.ToDisplayString());
        if (updateResult.IsFailure)
            return updateResult.Error;  

        var saveResult = await _locationRepository.UpdateAsync(locationResult.Value, ct);
        if (saveResult.IsFailure)
            return saveResult.Error;  

        return locationResult.Value.Id;
    }
}