using Microsoft.AspNetCore.Mvc;
using Shared;
namespace DirectoryService.Web;

public static class ResponseExt
{
    public static ActionResult ToResponse(this Failure failure)
    {
        if (!failure.Any())
        {
            return new ObjectResult(null)
            {
                StatusCode = StatusCodes.Status500InternalServerError,
            };
        }

        var distinctErrorTypes = failure
            .Select(x => x.Type)
            .Distinct()
            .ToList();

        int statusCode;
        if (distinctErrorTypes.Contains(ErrorType.VALIDATION))
            statusCode = StatusCodes.Status400BadRequest;
        else if (distinctErrorTypes.Contains(ErrorType.NOTFOUND))
            statusCode = StatusCodes.Status404NotFound;
        else if (distinctErrorTypes.Contains(ErrorType.CONFLICT))
            statusCode = StatusCodes.Status409Conflict;
        else
            statusCode = StatusCodes.Status500InternalServerError;

        return new ObjectResult(failure)
        {
            StatusCode = statusCode,
        };
    }
}