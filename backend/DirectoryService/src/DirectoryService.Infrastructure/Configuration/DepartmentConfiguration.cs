using DirectoryService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data;

namespace DirectoryService.Infrastructure;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("departments");
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Name).IsRequired().HasMaxLength(500).HasColumnName("name");
        builder.Property(d => d.Path).IsRequired().HasMaxLength(500).HasColumnName("path");
        builder.Property(d => d.CreatedAt).IsRequired().HasColumnType("timestamp with time zone").HasColumnName("created_at");
        builder.Property(d => d.UpdatedAt).IsRequired().HasColumnType("timestamp with time zone").HasColumnName("updated_at");

        builder.Property(d => d.ParentId).HasColumnName("parent_id");

        builder.Property(d => d.Slug)
            .IsRequired()
            .HasMaxLength(DepartmentSlug.MaxLength)
            .HasConversion(
                slug => slug.Value,
                value => DepartmentSlugFromDb(value))   // ← вызов метода, это expression
            .HasColumnName("slug");
    }

    private static DepartmentSlug DepartmentSlugFromDb(string value)
    {
        var result = DepartmentSlug.Create(value);
        if (result.IsFailure)
            throw new DataException($"В БД невалидный slug '{value}': {result.Error}");
        return result.Value;
    }
}