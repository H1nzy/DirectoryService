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
        string normalized = value.Trim().ToLowerInvariant();
            if (normalized.Length < MinLength || normalized.Length > MaxLength){
            throw new ArgumentException(
                $"slug (от {MinLength} до {MaxLength} символов)", nameof(value));
            }

        if (!SlugPattern().IsMatch(normalized)){
            throw new ArgumentException(
                "slug (только строчные латинские буквы, цифры и дефисы, " +
                "не начинается и не заканчивается дефисом)", nameof(value));
        }

        return new DepartmentSlug(normalized);
    }

    [GeneratedRegex(@"^[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", RegexOptions.None, matchTimeoutMilliseconds: 100)]
    private static partial Regex SlugPattern();
}