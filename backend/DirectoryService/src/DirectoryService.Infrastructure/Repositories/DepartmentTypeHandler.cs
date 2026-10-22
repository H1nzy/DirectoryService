using System.Data;
using Dapper;
using DirectoryService.Domain;

namespace DirectoryService.Infrastructure;

public class DepartmentSlugTypeHandler : SqlMapper.TypeHandler<DepartmentSlug>
{
    // Как сохранить в базу данных: берем чистую строку из свойства Value
    public override void SetValue(IDbDataParameter parameter, DepartmentSlug? value)
    {
        parameter.Value = value is null ? DBNull.Value : value.Value;
    }

    // Как прочитать из базы данных: берем строку и передаем в метод Create
    public override DepartmentSlug? Parse(object value)
    {
        if (value is null || value is DBNull) 
            return null;

        return DepartmentSlug.Create(value.ToString()!);
    }
}
