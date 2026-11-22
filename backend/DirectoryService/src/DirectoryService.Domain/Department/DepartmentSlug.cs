using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using Shared;

namespace DirectoryService.Domain;

public sealed partial record DepartmentSlug
{
    public const int MinLength = 2;
    public const int MaxLength = 100;

    public string Value { get; }

    private DepartmentSlug(string value)
    {
        Value = value;
    }

    public static Result<DepartmentSlug, Failure> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.Validation(
                "department.slug.is.empty",
                "Slug не может быть пустым",
                nameof(Value)).ToFailure();

        var normalized = value.Trim().ToLowerInvariant();

        if (normalized.Length < MinLength || normalized.Length > MaxLength)
            return Errors.Validation(
                "department.slug.length.is.invalid",
                $"Slug должен быть от {MinLength} до {MaxLength} символов",
                nameof(Value)).ToFailure();

        if (!SlugPattern().IsMatch(normalized))
            return Errors.Validation(
                "department.slug.format.is.invalid",
                "Slug содержит недопустимые символы",
                nameof(Value)).ToFailure();

        return Result.Success<DepartmentSlug, Failure>(new DepartmentSlug(normalized));
    }

    [GeneratedRegex(@"^[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", RegexOptions.None, matchTimeoutMilliseconds: 100)]
    private static partial Regex SlugPattern();
}