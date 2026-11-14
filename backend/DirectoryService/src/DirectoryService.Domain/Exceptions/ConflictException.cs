using System.Text.Json;
using Shared;
namespace DirectoryService.Domain;
public class ConflictException : Exception
{
    public ConflictException (Errors[] errors) : base(JsonSerializer.Serialize(errors))
    {
        
    }
}