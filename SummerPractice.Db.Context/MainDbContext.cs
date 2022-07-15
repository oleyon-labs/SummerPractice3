using Microsoft.EntityFrameworkCore;
using SummerPractice3.Db.Entities;

namespace SummerPractice.Db.Context
{
    public class MainDbContext : DbContext
    {
        public DbSet<Bank> Banks { get; set; }
        public DbSet<MeasurementUnit> MeasurementUnits { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<MaterialSupplier> MaterialSuppliers { get; set; }
        public DbSet<StorageUnit> StorageUnits { get; set; }
        public MainDbContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            Database.EnsureCreated();
        }
        public MainDbContext(bool clearScheme)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            if (clearScheme)
                Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=21455;Database=summerpractice3;Username=postgres;Password=password;");
        }
    }
}