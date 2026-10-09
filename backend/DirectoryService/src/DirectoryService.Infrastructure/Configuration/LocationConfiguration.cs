namespace DirectoryService.Infrastructure;
using System.ComponentModel.DataAnnotations;
using DirectoryService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
     public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.HasKey(l => l.Id);
        builder.Property(l => l.Name).IsRequired().HasMaxLength(500).HasColumnName("name");
        builder.Property(l => l.Address).IsRequired().HasMaxLength(500).HasColumnName("address");
        builder.Property(l => l.CreatedAt).IsRequired().HasColumnType("timestamp with time zone").HasColumnName("created_at");
        builder.Property(l => l.UpdatedAt).IsRequired().HasColumnType("timestamp with time zone").HasColumnName("updated_at");
    }
}