using System.ComponentModel.DataAnnotations;
using VehicleRental.API.Models.Vehicles;

namespace VehicleRental.API.Models.Reservations
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        public int VehicleId { get; set; }

        public Vehicle? Vehicle { get; set; }

        [Required]
        public required string UserId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Range(0, double.MaxValue)]
        public decimal TotalPrice { get; set; }

        public bool IsActive { get; set; } = true;
    }
}