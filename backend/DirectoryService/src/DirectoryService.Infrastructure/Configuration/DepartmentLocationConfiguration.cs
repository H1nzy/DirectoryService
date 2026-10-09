using DirectoryService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DirectoryService.Infrastructure;
public class DepartmentLocationConfiguration : IEntityTypeConfiguration<DepartmentLocation>
{
    public void Configure(EntityTypeBuilder<DepartmentLocation> builder)
    {
        builder.HasKey(dl => dl.Id);
        builder.HasOne<Department>().WithMany().HasForeignKey(dl => dl.DepartmentId).IsRequired();
        builder.HasOne<Location>().WithMany().HasForeignKey(dl => dl.LocationId).IsRequired();
        builder.Property(dl => dl.IsPrimary).HasDefaultValue(false).IsRequired();
        builder.HasIndex(dl => new {dl.DepartmentId, dl.LocationId}).IsUnique();
    }
}
