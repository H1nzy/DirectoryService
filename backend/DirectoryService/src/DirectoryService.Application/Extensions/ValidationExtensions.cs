using FluentValidation.Results;
using Shared;
namespace DirectoryService.Application;
public static class ValidationExtensions
{
    public static Failure ToErrors(this ValidationResult validationResult)
        => validationResult.Errors
            .Select(e => Errors.Validation(e.ErrorCode, e.ErrorMessage, e.PropertyName))
            .ToArray();
    
}