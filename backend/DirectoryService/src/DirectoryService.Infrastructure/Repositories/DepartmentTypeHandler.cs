using System.Data;
using Dapper;
using DirectoryService.Domain;

namespace DirectoryService.Infrastructure;

public class DepartmentSlugTypeHandler : SqlMapper.TypeHandler<DepartmentSlug>
{
    public override void SetValue(IDbDataParameter parameter, DepartmentSlug? value)
    {
        parameter.Value = value is null ? DBNull.Value : value.Value;
    }

    public override DepartmentSlug? Parse(object value)
    {
        if (value is null || value is DBNull)
            return null;

        var stringValue = value as string ?? value.ToString()!;
        var result = DepartmentSlug.Create(stringValue);
        if (result.IsFailure)
        {
            throw new DataException(
                $"В БД невалидный slug '{stringValue}': {result.Error}");
        }

        return result.Value;
    }
}