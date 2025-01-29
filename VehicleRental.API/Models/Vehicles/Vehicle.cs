using System.ComponentModel.DataAnnotations;

namespace VehicleRental.API.Models.Vehicles
{
    public abstract class Vehicle
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Brand { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Model { get; set; }

        [Required]
        [MaxLength(15)]
        public required string LicensePlate { get; set; }

        [Range(0, double.MaxValue)]
        public decimal RentalPricePerMinute { get; set; }
    }
}