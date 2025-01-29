namespace VehicleRental.API.Models.Vehicles
{
    public class Truck : Vehicle
    {
        public int CargoCapacity { get; set; }
        public int Axles { get; set; }
        public bool HasAirConditioning { get; set; }
    }
}