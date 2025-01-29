using Microsoft.EntityFrameworkCore;
using VehicleRental.API.Models.Reservations;
using VehicleRental.API.Models.Users;
using VehicleRental.API.Models.Vehicles;

namespace VehicleRental.API.Data
{
    public class VehicleRentalDbContext : DbContext
    {
        public VehicleRentalDbContext(DbContextOptions<VehicleRentalDbContext> options) : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vehicle>()
                .HasDiscriminator<string>("VehicleType")
                .HasValue<Car>("Car")
                .HasValue<Motorcycle>("Motorcycle")
                .HasValue<Truck>("Truck");

            modelBuilder.Entity<Car>()
                .Property(c => c.HasAirConditioning)
                .HasColumnName("HasAirConditioning");

            modelBuilder.Entity<Truck>()
                .Property(t => t.HasAirConditioning)
                .HasColumnName("HasAirConditioning");

            modelBuilder.Entity<Vehicle>()
                .Property(v => v.RentalPricePerMinute)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Reservation>()
                .Property(r => r.TotalPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Reservation>()
                .HasIndex(r => new { r.VehicleId, r.StartDate, r.EndDate })
                .IsUnique()
                .HasFilter("[IsActive] = 1");
        }
    }
}