namespace DirectoryService.Domain;

public class PositionNameValidationException : BadException
{
    public PositionNameValidationException(): base([PositionErrors.PositionName.ValidationException()])
    {
        
    }
}