using System.Text.Json;
using Shared;

namespace DirectoryService.Domain;

public class BadException : Exception 
{
    public BadException(Failure errors) : base(JsonSerializer.Serialize(errors))
    {
        
    }
    
}