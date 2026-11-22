namespace DirectoryService.Application;
using Shared;
public class LocationValidationException : BadRequestException
{
    public LocationValidationException(Failure errors): base(errors)
    {
        
    }
}