using System.Text.Json;
using Shared;

namespace DirectoryService.Application;

public class BadRequestException : Exception 
{
    public BadRequestException(Errors[] errors) : base(JsonSerializer.Serialize(errors))
    {
        
    }
    
}