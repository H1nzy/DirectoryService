namespace DirectoryService.Domain;
using Shared;
public static class PositionErrors
{
    public static class PositionName
    {
        public static Failure ValidationException() => Errors.Validation("position.name.is.space", $"The position has an empty name.");
    }
}