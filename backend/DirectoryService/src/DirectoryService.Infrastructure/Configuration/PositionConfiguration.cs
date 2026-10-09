namespace DirectoryService.Infrastructure;

using System.ComponentModel.DataAnnotations;
using DirectoryService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable("positions");
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Name).IsRequired().HasMaxLength(500).HasColumnName("name");

        builder.Property(p => p.CreatedAt).IsRequired().HasColumnType("timestamp with time zone").HasColumnName("created_at");

        builder.Property(p => p.UpdatedAt).IsRequired().HasColumnType("timestamp with time zone").HasColumnName("updated_at");
    }
}