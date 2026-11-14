using System.Text.Json;
using DirectoryService.Application;
using DirectoryService.Domain;
using Shared;

namespace DirectoryService.Web;
public class ExceptionMiddleware
{
    private readonly RequestDelegate  _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
           await HandleExceptionAsync(httpContext, ex);
        }
    }
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, exception.Message);
        (int code, Errors[]? errors) = exception switch
        {
            BadRequestException => (StatusCodes.Status400BadRequest, JsonSerializer.Deserialize<Errors[]>(exception.Message)),
            BadException => (StatusCodes.Status400BadRequest, JsonSerializer.Deserialize<Errors[]>(exception.Message)),
            NotFoundRequestException => (StatusCodes.Status404NotFound, JsonSerializer.Deserialize<Errors[]>(exception.Message)),
            NotFoundException => (StatusCodes.Status404NotFound, JsonSerializer.Deserialize<Errors[]>(exception.Message)),
            ConflictRequestException => (StatusCodes.Status409Conflict, JsonSerializer.Deserialize<Errors[]>(exception.Message)),
            ConflictException => (StatusCodes.Status409Conflict, JsonSerializer.Deserialize<Errors[]>(exception.Message)),
            _ => (StatusCodes.Status500InternalServerError, [Errors.Failure(null, "Something went wrong")])
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = code;
        await context.Response.WriteAsJsonAsync(errors);
    }
}

public static class ExceptionMiddlewareExtension
{
    public static IApplicationBuilder UseExceptionMiddleware(this WebApplication app) => app.UseMiddleware<ExceptionMiddleware>();
}