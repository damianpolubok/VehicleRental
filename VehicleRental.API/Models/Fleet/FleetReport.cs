namespace VehicleRental.API.Models.Fleet
{
    public class FleetReport
    {
        public int TotalVehicles { get; set; }
        public int AvailableVehicles { get; set; }
        public int RentedVehicles { get; set; }
        public int TotalReservations { get; set; }
        public int ActiveReservations { get; set; }
        public int CanceledReservations { get; set; }
    }
}