using System.Text.Json;
using Shared;

namespace DirectoryService.Application;
public class NotFoundRequestException : Exception
{
    public NotFoundRequestException(Errors[] errors) : base(JsonSerializer.Serialize(errors))
    {

    }
}