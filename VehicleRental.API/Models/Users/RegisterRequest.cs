namespace VehicleRental.API.Models.Users
{
    public class RegisterRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}