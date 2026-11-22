using System.Text.Json;
using Shared;

namespace DirectoryService.Application;

public class BadRequestException : Exception 
{
    public BadRequestException(Failure errors) : base(JsonSerializer.Serialize(errors))
    {
        
    }
    
}