using Microsoft.EntityFrameworkCore;
using ParcelTrackingSystem.Models;

namespace ParcelTrackingSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Parcel> Parcels { get; set; }
    }
}