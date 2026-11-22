namespace Shared;
using System.Text.Json.Serialization;
public record Errors
{
    public static Errors None { get; } = new Errors(string.Empty, string.Empty, ErrorType.NONE, null);
    public string Code {get; }
    public string Message {get; }
    public ErrorType Type {get; }
    public string? InvalidField {get; }

    [JsonConstructor]
    private Errors(string code, string message, ErrorType type, string? invalidfield = null)
    {
        Code = code;
        Message = message;
        Type = type;
        InvalidField = invalidfield;
    }

    public static Errors NotFound(string? code, string message)
        => new(code ?? "record.not.found", message, ErrorType.NOTFOUND);
    
    public static Errors Validation(string? code, string message, string? invalidField = null)
        => new(code ?? "value.is.invalid", message, ErrorType.VALIDATION, invalidField);
    public static Errors Conflict(string? code, string message)
        => new(code ??"value.is.conflict", message, ErrorType.CONFLICT);
    public static Errors Failure(string? code, string message)
        => new(code ?? "failure", message, ErrorType.FAILURE);

     public Failure ToFailure() => this;
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ErrorType
{
    NONE,
    VALIDATION,
    NOTFOUND,
    FAILURE,
    CONFLICT,
}