namespace VehicleRental.API.Models.Vehicles
{
    public class Car : Vehicle
    {
        public int Seats { get; set; }
        public bool HasAirConditioning { get; set; }
    }
}