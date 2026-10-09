using DirectoryService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DirectoryService.Infrastructure;
public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Name).IsRequired().HasMaxLength(500).HasColumnName("name");
        builder.Property(d => d.Path).IsRequired().HasMaxLength(500).HasColumnName("path");
        builder.Property(d => d.CreatedAt).IsRequired().HasColumnType("timestamp with time zone").HasColumnName("created_at");
        builder.Property(d => d.UpdatedAt).IsRequired().HasColumnType("timestamp with time zone").HasColumnName("updated_at");
builder.Property(d => d.Slug).IsRequired().HasMaxLength(DepartmentSlug.MaxLength).HasConversion(d => d.Value, value => DepartmentSlug.Create(value)).HasColumnName("slug");
        builder.Property(d => d.Slug).IsRequired().HasMaxLength(DepartmentSlug.MaxLength).HasConversion(d => d.Value, value => DepartmentSlug.Create(value));
    }
}