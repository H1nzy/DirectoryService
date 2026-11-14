using System.Text.Json;
using Shared;

namespace DirectoryService.Domain;

public class BadException : Exception 
{
    public BadException(Errors[] errors) : base(JsonSerializer.Serialize(errors))
    {
        
    }
    
}