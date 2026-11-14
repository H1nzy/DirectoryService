namespace DirectoryService.Application;
using Shared;

public static partial class LocationErrors
{
    public static class Locations
    {
        public static Errors ConflictRequestException(string name) => Errors.Conflict(
                    "location.name.conflict",
                    $"Location with name '{name}' already exists.");
        public static Errors NotFoundRequestException(Guid id, string name) => Errors.NotFound(
                    "location.name.and.id.not.found",
                    $"Location with name '{name}' and id '{id}' not found.");
    }
}