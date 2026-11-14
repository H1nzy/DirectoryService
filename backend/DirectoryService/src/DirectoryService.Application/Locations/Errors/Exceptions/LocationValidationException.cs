namespace DirectoryService.Application;
using Shared;
public class LocationValidationException : BadRequestException
{
    public LocationValidationException(Errors[] errors): base(errors)
    {
        
    }
}