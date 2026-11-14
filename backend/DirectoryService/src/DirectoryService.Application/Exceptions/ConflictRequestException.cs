using System.Text.Json;
using Shared;
namespace DirectoryService.Application;
public class ConflictRequestException : Exception
{
    public ConflictRequestException (Errors[] errors) : base(JsonSerializer.Serialize(errors))
    {
        
    }
}