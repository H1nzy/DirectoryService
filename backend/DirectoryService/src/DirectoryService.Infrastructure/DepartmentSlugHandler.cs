using Dapper;
using DirectoryService.Domain;
using System.Data;

namespace DirectoryService.Infrastructure.Dapper;

public class DepartmentSlugHandler : SqlMapper.TypeHandler<DepartmentSlug>
{
    public override DepartmentSlug Parse(object? value)
    {
        if (value is not string stringValue)
        {
            throw new DataException(
                $"Не удалось преобразовать значение типа {value?.GetType().Name ?? "null"} в DepartmentSlug");
        }

        return DepartmentSlug.Create(stringValue);
    }

    public override void SetValue(IDbDataParameter parameter, DepartmentSlug? value)
    {
        if (value is null)
        {
            parameter.Value = DBNull.Value;
            return;
        }

        parameter.Value = value.Value; 
    }
}