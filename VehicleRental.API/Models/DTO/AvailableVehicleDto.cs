namespace VehicleRental.API.Features.Reservations.Queries
{
    public class AvailableVehicleDto
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string LicensePlate { get; set; } = string.Empty;
        public decimal RentalPricePerMinute { get; set; }
        public string VehicleType { get; set; } = string.Empty;
        public int? Seats { get; set; }
        public bool? HasAirConditioning { get; set; }
        public bool? HasSidecar { get; set; }
        public int? CargoCapacity { get; set; }
        public int? Axles { get; set; }
    }
}