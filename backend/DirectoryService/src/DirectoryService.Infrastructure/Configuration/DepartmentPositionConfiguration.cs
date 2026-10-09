using DirectoryService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DirectoryService.Infrastructure;
public class DepartmentPositionConfiguration : IEntityTypeConfiguration<DepartmentPosition>
{
    public void Configure(EntityTypeBuilder<DepartmentPosition> builder)
    {
        builder.HasKey(dp => dp.Id);
        builder.HasOne<Department>().WithMany().HasForeignKey(dp => dp.DepartmentId).IsRequired();
        builder.HasOne<Position>().WithMany().HasForeignKey(dp => dp.PositionId).IsRequired();
        builder.HasIndex(dp => new{dp.DepartmentId, dp.PositionId}).IsUnique();
    }
}