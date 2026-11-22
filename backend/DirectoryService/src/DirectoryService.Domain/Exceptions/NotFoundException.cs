using System.Text.Json;
using Shared;

namespace DirectoryService.Domain;
public class NotFoundException : Exception
{
    public NotFoundException(Failure errors) : base(JsonSerializer.Serialize(errors))
    {

    }
}