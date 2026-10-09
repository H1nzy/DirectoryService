using DirectoryService.Domain;
using Microsoft.EntityFrameworkCore;
namespace DirectoryService.Infrastructure;

    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentLocation> DepartmentLocations { get; set; }
        public DbSet<DepartmentPosition> DepartmentPositions { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Position> Positions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
        }
    
    
    }

