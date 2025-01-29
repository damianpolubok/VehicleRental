using MediatR;
using System.ComponentModel.DataAnnotations;
using VehicleRental.API.Models.Vehicles;

namespace VehicleRental.API.Features.Vehicles.Commands
{
    public class CreateVehicleCommand : IRequest<Vehicle>
    {
        [Required]
        [RegularExpression("^(Car|Truck|Motorcycle)$", ErrorMessage = "Invalid Vehicle Type.")]
        public required string VehicleType { get; set; }

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

        public int? Seats { get; set; }
        public bool? HasAirConditioning { get; set; }
        public int? CargoCapacity { get; set; }
        public int? Axles { get; set; }
        public bool? HasSidecar { get; set; }
    }
}