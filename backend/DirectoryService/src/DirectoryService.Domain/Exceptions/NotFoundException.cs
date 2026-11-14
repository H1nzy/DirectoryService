using System.Text.Json;
using Shared;

namespace DirectoryService.Domain;
public class NotFoundException : Exception
{
    public NotFoundException(Errors[] errors) : base(JsonSerializer.Serialize(errors))
    {

    }
}