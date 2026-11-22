using DirectoryService.Contracts;
using FluentValidation;
using Shared;
using CSharpFunctionalExtensions;
using DirectoryService.Domain;

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

    public async Task<Result<Guid, Failure>> Create(CreateLocationDto createLocationDto, CancellationToken ct = default)
    {
        var validationResult = await _validator.ValidateAsync(createLocationDto, ct);
        if (!validationResult.IsValid)
            return validationResult.ToErrors();

        var isNameOccupiedResult = await _locationRepository.CheckName(createLocationDto.Name, ct);
        if (isNameOccupiedResult.IsFailure)
            return isNameOccupiedResult.Error;           // 500 — сбой проверки

        if (isNameOccupiedResult.Value)
            return LocationErrors.Locations.LocationConflictRequestException(createLocationDto.Name); // 409

        var locationResult = Location.Create(
            createLocationDto.Name,
            createLocationDto.Address.ToDisplayString());

        if (locationResult.IsFailure)
            return locationResult.Error;                  // 400 VALIDATION

        var addResult = await _locationRepository.AddAsync(locationResult.Value, ct);
        if (addResult.IsFailure)
            return addResult.Error;                       // 500 FAILURE

        return addResult.Value;
    }
}