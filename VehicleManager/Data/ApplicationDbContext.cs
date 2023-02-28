using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace VehicleManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }
        public DbSet<VehicleManager.Models.Car> Cars { get; set; } = default!;
        public DbSet<VehicleManager.Models.VehicleCategory> VehicleCategories { get; set; } = default!;
        public DbSet<VehicleManager.Models.Rental> Rentals { get; set; } = default!;

    }
}
