using System.Text.RegularExpressions;

namespace DirectoryService.Domain;
public  sealed partial record DepartmentSlug
{
    public const int MinLength = 2;
    public const int MaxLength = 100;
    public string Value{get;}

    private DepartmentSlug(string value)
    {
        Value = value;
    }

     public static DepartmentSlug Create(string value)
    {
         if (string.IsNullOrWhiteSpace(value))
         {
             throw new SlugValidationException();
        }
        string normalized = value.Trim().ToLowerInvariant();
            if (normalized.Length < MinLength || normalized.Length > MaxLength){
            throw new DepartmentSlugValidationException(normalized);
            }

        if (!SlugPattern().IsMatch(normalized)){
            throw new DepartmentSlugValidationException(normalized);
        }

        return new DepartmentSlug(normalized);
    }

    [GeneratedRegex(@"^[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", RegexOptions.None, matchTimeoutMilliseconds: 100)]
    private static partial Regex SlugPattern();
}